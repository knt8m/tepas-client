using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableObject/Stage", fileName = "Stage")]
public class Stage : ScriptableObject
{
    [SerializeField]
    public string Number = "1";

    [SerializeField]
    public int Remain = 1;

    [SerializeField]
    public int Size = 32;

    [SerializeField]
    public int X = 10;

    [SerializeField]
    public int Y = 5;

    public List<TabletRow> Rows;

    public List<TabletRow> GetRows()
    {
        return this.Rows;
    }

    public void SetRows(List<TabletRow> Rows)
    {
        this.Rows = Rows;
    }

    public static Stage CreateScriptableObjectFromJSON(string json)
    {
        Stage data = CreateInstance<Stage>();
        JsonUtility.FromJsonOverwrite(json, data);
        return data;
    }
}

[Serializable]
public class TabletRow
{
    public List<TabletData> Columns;
}


[Serializable]
public class TabletData
{
    [LabelText("åªèÛ"), Required]
    public TabletState currentState;

    [LabelText("âÒìö")]
    public TabletState answerState;

    public int hintNum;
}
