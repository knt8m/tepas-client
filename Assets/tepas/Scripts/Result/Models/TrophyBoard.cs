using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;


[CreateAssetMenu(menuName = "ScriptableObject/TrophyBoard", fileName = "TrophyBoard")]
public class TrophyBoard : ScriptableObject
{
    public List<Trophy> trophies;

    public static TrophyBoard CreateScriptableObjectFromJSON(string json)
    {
        TrophyBoard data = CreateInstance<TrophyBoard>();
        JsonUtility.FromJsonOverwrite(json, data);
        return data;
    }

    public void Show()
    {
        if (trophies == null || trophies.Count == 0)
        {
            Debug.Log("No scores to display.");
            return;
        }
        foreach (Trophy trophy in trophies)
        {
            Debug.Log($"no {trophy.no} , border score: {trophy.borderScore}");
        }
    }
}