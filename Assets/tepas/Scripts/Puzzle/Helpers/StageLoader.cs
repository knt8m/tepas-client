using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

public class StageLoader : MonoBehaviour
{
    [SerializeField]
    GameObject stage;

    [SerializeField]
    Stage stageData;

    string stageNum;


    private string tabletPath = "Visual/Misc/Tablet";

    public string GetFullPath()
    {
#if UNITY_EDITOR
        // UnityEditor環境
        return $"{Application.dataPath}/tepas/Resources/Data/Stage{stageNum}.json";
#else
        // ビルド後環境（Resourcesフォルダを使用する場合）
        return $"Resources/Data/Stage{stageNum}.json";
#endif
    }

    public string GetFullPathOld()
    {
#if !UNITY_EDITOR
        string basePath = Application.persistentDataPath;
#else
        string basePath = Application.dataPath;
#endif
        string dataPath = "tepas/Resources/Data";
        return $"{basePath}/{dataPath}/Stage{stageNum}.json";
    }

    public Stage GetStageData()
    {
        return stageData;
    }

    [BoxGroup("デバッグ"), Button("全削除", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void AllDestroy()
    {
        foreach (Transform child in stage.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public bool LoadJson()
    {
        bool result = false;
        string fullPath = GetFullPath();
        string json = string.Empty;
#if UNITY_EDITOR
        json = System.IO.File.ReadAllText(fullPath);
#else
        string resourcePath = fullPath.Replace("Resources/", "").Replace(".json", "");
        TextAsset jsonFile = Resources.Load<TextAsset>(resourcePath);
        if (jsonFile != null)
        {
            json = jsonFile.text;
        }
#endif
        try
        {
            stageData = Stage.CreateScriptableObjectFromJSON(json);
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load settings data: " + e.Message);
        }
        return result;
    }

    [BoxGroup("デバッグ"), Button("ロード", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void DebugLoadStageData()
    {
        stageNum = stageData.Number.ToString();
        LoadStageData();
    }

    public void LoadStageData(int stageNo)
    {
        stageNum = Convert.ToString(stageNo);
        LoadStageData();
    }


    public void LoadStageData()
    {
        if (LoadJson())
        {
            Debug.Log("ステージの読込に成功しました。");
        }
        else
        {
            Debug.LogError("ステージの読込に失敗しました。");
            return;
        }
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        if (stageData == null)
        {
            Debug.Log("ステージデータが設定されてません。");
            return;
        }
        int size = stageData.Size;
        int x = stageData.X;
        int y = stageData.Y;
        if (size == 0 || x == 0 || y == 0)
        {
            Debug.Log("不正なデータがあります。");
            Debug.Log("Size:" + size);
            Debug.Log("X:" + x);
            Debug.Log("Y:" + y);
            return;
        }
        if (size % 2 != 0)
        {
            Debug.LogError("Sizeは偶数を指定してください。");
            Debug.Log("Size:" + size);
        }
        float positionYg = 179f; //Y偶数
        float positionYk = 179f - size / 2; //Y奇数

        List<TabletRow> rows = stageData.GetRows();
        int rowIndex = 0;
        int columnIndex = 0;
        foreach (TabletRow row in rows)
        {
            positionX = -400f;
            columnIndex = 0;
            foreach (TabletData data in row.Columns)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(tabletPath);
                GameObject tabletObj;
                if (columnIndex % 2 == 0)
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYg, 0.0f), Quaternion.identity);
                }
                else
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYk, 0.0f), Quaternion.identity);
                }
                //GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletUI tabletUi = tabletObj.GetComponent<TabletUI>();
                if (data.currentState == TabletState.NoneEditor)
                {
                    data.currentState = TabletState.None;
                }
                if (data.answerState == TabletState.NoneEditor)
                {
                    data.answerState = TabletState.None;
                }
                Debug.Log("currentState:" + data.currentState);
                tabletUi.currentState = data.currentState;
                tabletUi.answerState = data.answerState;
                tabletUi.hintNum = data.hintNum;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
                columnIndex++;
            }
            positionYg = positionYg - size;
            positionYk = positionYk - size;
            rowIndex++;
        }
    }

}
