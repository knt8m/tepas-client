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
        string json = System.IO.File.ReadAllText(fullPath);
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
            Debug.Log("�X�R�A�{�[�h�̓Ǎ��ɐ������܂����B");
            return timeScoreBoard;
        }
        else
        {
            Debug.LogError("�X�R�A�{�[�h�̓Ǎ��Ɏ��s���܂����B");
            return null;
        }
    }
}
