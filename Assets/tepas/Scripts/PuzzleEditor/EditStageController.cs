using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

public class EditStageController : MonoBehaviour
{
    [SerializeField]
    GameObject stage;

    [SerializeField]
    Stage stageData;

    [SerializeField]
    TextMeshProUGUI modeText;

    private string editableTabletPath = "Visual/Misc/EditableTablet";

    public int GetRemainNum()
    {
        int count = 0;
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                TabletEditorUI tabletUi = child.gameObject.GetComponent<TabletEditorUI>();
                if(tabletUi.currentState == TabletState.Unknown)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void SaveRemain()
    {
        int remainNum = GetRemainNum();
        stageData.Remain = remainNum;
    }

    public string GetFullPath()
    {
#if !UNITY_EDITOR
        string basePath = Application.persistentDataPath;
#else
        string basePath = Application.dataPath;
#endif
        string dataPath = "tepas/Resources/Data";
        string stageNum = stageData.Number.ToString();
        return $"{basePath}/{dataPath}/Stage{stageNum}.json";
    }

    public bool SaveJson()
    {
        bool result = false;
        string fullPath = GetFullPath();
        string json = JsonUtility.ToJson(stageData);
        Debug.Log("SaveSettings: " + fullPath);
        try
        {
            System.IO.File.WriteAllText(fullPath, json);
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save settings data: " + e.Message);
        }
        return result;
    }

    public bool LoadJson()
    {
        bool result = false;
        string fullPath = GetFullPath();
        string json = System.IO.File.ReadAllText(fullPath);
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



    [BoxGroup("デバッグ"), Button("現在のボタン", ButtonSizes.Medium), GUIColor(1, 0, 0, 1)]
    public void OnClickCurrentButton()
    {
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                TabletEditorUI tabletUi = child.gameObject.GetComponent<TabletEditorUI>();
                tabletUi.ApplyCurrentState();
                tabletUi.status = TabletPhase.Current;
            }
        }
        modeText.text = "問題モード";
    }

    [BoxGroup("デバッグ"), Button("回答のボタン", ButtonSizes.Medium), GUIColor(1, 0, 0, 1)]
    public void OnClickAnswerButton()
    {
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                TabletEditorUI tabletUi = child.gameObject.GetComponent<TabletEditorUI>();
                tabletUi.ApplyAnswerState();
                tabletUi.status = TabletPhase.Answer;
            }
        }
        modeText.text = "回答モード";
    }


    [BoxGroup("デバッグ"), Button("初期化", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void InitStageData()
    {
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
            Debug.LogError("不正なデータがあります。");
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

        List<TabletRow> rows = new List<TabletRow>();
        TabletRow tabletRow = new TabletRow();
        List<TabletData> columns = new List<TabletData>();
        TabletData tabletData = new TabletData();
        tabletData.currentState = TabletState.None;
        tabletData.answerState = TabletState.None;
        tabletData.hintNum = 0;

        for (int j = 0; j < y; j++)
        {
            positionX = -400f;
            columns = new List<TabletData>();
            for (int i = 0; i < x; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj;
                if (i % 2 == 0)
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYg, 0.0f), Quaternion.identity);
                }
                else
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYk, 0.0f), Quaternion.identity);
                }
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.NoneEditor;
                tabletUi.answerState = TabletState.NoneEditor;
                tabletUi.row = j;
                tabletUi.column = i;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
                columns.Add(tabletData);
            }
            //positionY = positionY - size;
            positionYg = positionYg - size;
            positionYk = positionYk - size;
            tabletRow.Columns = columns;
            rows.Add(tabletRow);
        }
        stageData.SetRows(rows);
    }

    [BoxGroup("デバッグ"), Button("問題→回答", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void SaveQuestionToAnswer()
    {
        List<TabletEditorUI> tablets = new List<TabletEditorUI>();
        int index = 0;
        int tabletsMax = 0;
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                tablets.Add(child.gameObject.GetComponent<TabletEditorUI>());
                index++;
            }
        }
        tabletsMax = index;
        for (int i = 0; i < tabletsMax - 1; i++)
        {
            for (int j = i + 1; j < tabletsMax; j++)
            {
                // rowが小さい順に並べる
                if (tablets[i].row > tablets[j].row ||
                    (tablets[i].row == tablets[j].row && tablets[i].column > tablets[j].column))
                {
                    // tablets[i] と tablets[j] を交換する
                    var temp = tablets[i];
                    tablets[i] = tablets[j];
                    tablets[j] = temp;
                }
            }
        }
        int maxRow = 0;
        int maxColumn = 0;
        foreach (var tablet in tablets)
        {
            //Debug.Log("row: " + tablet.row);
            //Debug.Log("column: " + tablet.column);
            if (tablet.row > maxRow)
            {
                maxRow = tablet.row;//最大Row
            }
            if (tablet.column > maxColumn)
            {
                maxColumn = tablet.column;//最大Column
            }
        }

        Debug.Log("maxRow: " + maxRow);
        Debug.Log("maxColumn: " + maxColumn);
        Debug.Log("==========================TabletData==========================");
        List<TabletRow> rows = new List<TabletRow>();
        TabletRow tabletRow = new TabletRow();
        List<TabletData> columns = new List<TabletData>();
        TabletData tabletData = new TabletData();
        index = 0;
        while (true)
        {
            if (index >= tabletsMax)
            {
                Debug.Log(index);
                break;
            }
            tabletData = new TabletData();
            tabletData.currentState = tablets[index].currentState;
            tabletData.answerState = tablets[index].currentState;
            tabletData.hintNum = tablets[index].hintNum;
            columns.Add(tabletData);
            Debug.Log("Row: " + tablets[index].row);
            Debug.Log("Column: " + tablets[index].column);
            if (tablets[index].column == maxColumn)
            {
                tabletRow.Columns = columns;
                rows.Add(tabletRow);
                tabletRow = new TabletRow();
                columns = new List<TabletData>();
                index++;
                continue;
            }
            index++;
        }
        stageData.SetRows(rows);
        SaveRemain();
        if (SaveJson())
        {
            Debug.Log("ステージの保存に成功しました。");
        }
        else
        {
            Debug.LogError("ステージの保存に失敗しました。");
        }
        LoadStageData();
    }

        [BoxGroup("デバッグ"), Button("セーブ", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void SaveStageData()
    {
        List<TabletEditorUI> tablets = new List<TabletEditorUI>();
        int index = 0;
        int tabletsMax = 0;
        foreach (Transform child in stage.gameObject.transform)
        {
            if(child.gameObject.name == "EditableTablet(Clone)")
            {
                tablets.Add(child.gameObject.GetComponent<TabletEditorUI>());
                index++;
            }
        }
        tabletsMax = index;
        for (int i = 0; i < tabletsMax - 1; i++)
        {
            for (int j = i + 1; j < tabletsMax; j++)
            {
                // rowが小さい順に並べる
                if (tablets[i].row > tablets[j].row ||
                    (tablets[i].row == tablets[j].row && tablets[i].column > tablets[j].column))
                {
                    // tablets[i] と tablets[j] を交換する
                    var temp = tablets[i];
                    tablets[i] = tablets[j];
                    tablets[j] = temp;
                }
            }
        }
        int maxRow = 0;  
        int maxColumn = 0;
        foreach (var tablet in tablets)
        {
            //Debug.Log("row: " + tablet.row);
            //Debug.Log("column: " + tablet.column);
            if (tablet.row > maxRow)
            {
                maxRow = tablet.row;//最大Row
            }
            if (tablet.column > maxColumn)
            {
                maxColumn = tablet.column;//最大Column
            }
        }

        Debug.Log("maxRow: " + maxRow);
        Debug.Log("maxColumn: " + maxColumn);
        Debug.Log("==========================TabletData==========================");
        List<TabletRow> rows = new List<TabletRow>();
        TabletRow tabletRow = new TabletRow();
        List<TabletData> columns = new List<TabletData>();
        TabletData tabletData = new TabletData();
        index = 0;
        while (true)
        {
            if (index >= tabletsMax)
            {
                Debug.Log(index);
                break;
            }
            tabletData = new TabletData();
            tabletData.currentState = tablets[index].currentState;
            tabletData.answerState = tablets[index].answerState;
            tabletData.hintNum = tablets[index].hintNum;
            columns.Add(tabletData);
            Debug.Log("Row: " + tablets[index].row);
            Debug.Log("Column: " + tablets[index].column);
            if (tablets[index].column == maxColumn)
            {
               tabletRow.Columns = columns;
               rows.Add(tabletRow);
               tabletRow = new TabletRow();
               columns = new List<TabletData>();
               index++;
               continue;
           }
           index++;
       }
       stageData.SetRows(rows);
       SaveRemain();
        if (SaveJson())
        {
            Debug.Log("ステージの保存に成功しました。");
        }
        else
        {
            Debug.LogError("ステージの保存に失敗しました。");
        }
    }

    [BoxGroup("デバッグ"), Button("ロード", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
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
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
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
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                Debug.Log("currentState:");
                Debug.Log(data.currentState);
                if(data.currentState == TabletState.None)
                {
                    data.currentState = TabletState.NoneEditor;
                }
                if (data.answerState == TabletState.None)
                {
                    data.answerState = TabletState.NoneEditor;
                }
                tabletUi.currentState = data.currentState;
                tabletUi.answerState = data.answerState;
                tabletUi.hintNum = data.hintNum;
                tabletUi.row = rowIndex;
                tabletUi.column = columnIndex;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
                columnIndex++;
            }
            //positionY = positionY - size;
            positionYg = positionYg - size;
            positionYk = positionYk - size;
            rowIndex++;
        }
    }

    [BoxGroup("デバッグ"), Button("テスト", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void Test()
    {
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
        /*
        List<Field> fields = stageData.field;
        foreach (Field field in fields)
        {
            foreach(FieldX fieldX in field.fieldx)
            {

            }
        }
        */
        for (int j = 0; j < y; j++)
        {
            positionX = -400f;
            for (int i = 0; i < x; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.NoneEditor;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
            }
            positionY = positionY - size;
        }
    }

    [BoxGroup("デバッグ"), Button("テストxx", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void Test01()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;

        if(stageData == null)
        {
            Debug.Log("ステージデータが設定されてません。");
            return;
        }
        int size = stageData.Size;
        int x = stageData.X;  
        int y = stageData.Y;
        if(size == 0 || x == 0 || y == 0)
        {
            Debug.Log("不正なデータがあります。");
            Debug.Log("Size:" + size);
            Debug.Log("X:" + x);
            Debug.Log("Y:" + y);
            return;
        }

        for (int j = 0; j < y; j++)
        {
            positionX = -400f;
            for (int i = 0; i < x; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.NoneEditor;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
            }
            positionY = positionY - size;
        }
    }

    [BoxGroup("デバッグ"), Button("ステージ表示", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void ShowStage()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        for (int j = 0; j < 10; j++)
        {
            positionX = -400f;
            for (int i = 0; i < 26; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                positionX = positionX + 32;
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.None;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                //Debug.Log(tabletUi);
            }
            positionY = positionY - 32;
        }
    }

    [BoxGroup("デバッグ"), Button("ステージ表示32", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void ShowStage32()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        for (int j = 0; j < 13; j++)
        {
            positionX = -400f;
            for (int i = 0; i < 26; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load("Prefabs/Tablet32");
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                positionX = positionX + 32;
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.None;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                //Debug.Log(tabletUi);
            }
            positionY = positionY - 32;
        }
    }

    [BoxGroup("デバッグ"), Button("ステージ表示50", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void ShowStage50()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        for (int j = 0; j < 9; j++)
        {
            positionX = -400f;
            for (int i = 0; i < 17; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load("Prefabs/Tablet50");
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                positionX = positionX + 50;
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.None;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                //Debug.Log(tabletUi);
            }
            positionY = positionY - 50;
        }
    }

    [BoxGroup("デバッグ"), Button("全削除", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void AllDestroy()
    {
        foreach (Transform child in stage.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
