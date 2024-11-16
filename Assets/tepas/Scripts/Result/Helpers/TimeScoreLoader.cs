using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TimeScoreLoader : MonoBehaviour
{
    [SerializeField]
    TimeScoreBoard timeScoreBoard;

    private string GetFullPath()
    {
#if UNITY_EDITOR
        // UnityEditor環境
        return $"{Application.dataPath}/tepas/Resources/Config/timeScoreBoard.json";
#else
        // ビルド後環境（Resourcesフォルダを使用する場合）
        return $"Resources/Config/timeScoreBoard.json";
#endif
    }
    private string GetFullPathOld()
    {
#if !UNITY_EDITOR
        string basePath = Application.persistentDataPath;
#else
        string basePath = Application.dataPath;
#endif
        string dataPath = "tepas/Resources/Config";
        return $"{basePath}/{dataPath}/timeScoreBoard.json";
    }

    private bool LoadJson()
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
            timeScoreBoard = TimeScoreBoard.CreateScriptableObjectFromJSON(json);
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load settings data: " + e.Message);
        }
        return result;
    }

    public TimeScoreBoard Load()
    {
        if (LoadJson())
        {
            Debug.Log("スコアボードの読込に成功しました。");
            return timeScoreBoard;
        }
        else
        {
            Debug.LogError("スコアボードの読込に失敗しました。");
            return null;
        }
    }
}
