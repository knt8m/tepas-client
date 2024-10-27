using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MissScoreLoader : MonoBehaviour
{
    [SerializeField]
    MissScoreBoard missScoreBoard;

    private string GetFullPath()
    {
#if !UNITY_EDITOR
        string basePath = Application.persistentDataPath;
#else
        string basePath = Application.dataPath;
#endif
        string dataPath = "tepas/Resources/Config";
        return $"{basePath}/{dataPath}/MissScoreBoard.json";
    }

    private bool LoadJson()
    {
        bool result = false;
        string fullPath = GetFullPath();
        string json = System.IO.File.ReadAllText(fullPath);
        try
        {
            missScoreBoard = MissScoreBoard.CreateScriptableObjectFromJSON(json);
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load settings data: " + e.Message);
        }
        return result;
    }

    public MissScoreBoard Load()
    {
        if (LoadJson())
        {
            Debug.Log("スコアボードの読込に成功しました。");
            return missScoreBoard;
        }
        else
        {
            Debug.LogError("スコアボードの読込に失敗しました。");
            return null;
        }
    }
}
