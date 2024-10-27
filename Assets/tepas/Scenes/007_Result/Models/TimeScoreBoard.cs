using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;


[CreateAssetMenu(menuName = "ScriptableObject/TimeScoreBoard", fileName = "TimeScoreBoard")]
public class TimeScoreBoard : ScriptableObject
{
    public List<TimeScore> timeScores;

    public static TimeScoreBoard CreateScriptableObjectFromJSON(string json)
    {
        TimeScoreBoard data = CreateInstance<TimeScoreBoard>();
        JsonUtility.FromJsonOverwrite(json, data);
        return data;
    }

    public void Show()
    {
        if (timeScores == null || timeScores.Count == 0)
        {
            Debug.Log("No scores to display.");
            return;
        }
        foreach (TimeScore score in timeScores)
        {
            Debug.Log($"time {score.startTime} - {score.endTime}, score: {score.score}");
        }
    }
}
