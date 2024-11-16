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
#if UNITY_EDITOR
        // UnityEditor��
        return $"{Application.dataPath}/tepas/Resources/Config/MissScoreBoard.json";
#else
        // �r���h����iResources�t�H���_���g�p����ꍇ�j
        return $"Resources/Config/MissScoreBoard.json";
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
        return $"{basePath}/{dataPath}/MissScoreBoard.json";
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
            Debug.Log("�X�R�A�{�[�h�̓Ǎ��ɐ������܂����B");
            return missScoreBoard;
        }
        else
        {
            Debug.LogError("�X�R�A�{�[�h�̓Ǎ��Ɏ��s���܂����B");
            return null;
        }
    }
}
