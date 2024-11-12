using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;


[CreateAssetMenu(menuName = "ScriptableObject/MissScoreBoard", fileName = "MissScoreBoard")]
public class MissScoreBoard : ScriptableObject
{
    public List<MissScore> missScores;

    public static MissScoreBoard CreateScriptableObjectFromJSON(string json)
    {
        MissScoreBoard data = CreateInstance<MissScoreBoard>();
        JsonUtility.FromJsonOverwrite(json, data);
        return data;
    }

    public void Show()
    {
        if (missScores == null || missScores.Count == 0)
        {
            Debug.Log("No scores to display.");
            return;
        }
        foreach (MissScore score in missScores)
        {
            Debug.Log($"miss {score.startMiss} - {score.endMiss}, score: {score.score}");
        }
    }
}