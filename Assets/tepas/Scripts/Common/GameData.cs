using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    //ステージ進捗情報
    [SerializeField]
    public List<ClearStage> saveStageData1 = new List<ClearStage>();
    public List<OwnTrophy> ownTrophyData1;
    public string data1SavedDate;

    [SerializeField]
    public List<ClearStage> saveStageData2 = new List<ClearStage>();
    public List<OwnTrophy> ownTrophyData2;
    public string data2SavedDate;

    [SerializeField]
    public List<ClearStage> saveStageData3 = new List<ClearStage>();
    public List<OwnTrophy> ownTrophyData3;
    public string data3SavedDate;

    //設定情報
    public float bgmVolume;
    public bool bgmMute;
    public float seVolume;
    public bool seMute;
    public string mouseLeftClick;

    public GameData()
    {
        saveStageData1 = new List<ClearStage>();
        saveStageData2 = new List<ClearStage>();
        saveStageData3 = new List<ClearStage>();
        //ownTrophies = new List<OwnTrophy>();

        bgmVolume = 0.6f;
        bgmMute = false;
        seVolume = 0.6f;
        bgmMute = false;
        mouseLeftClick = "blue_tablet";
    }
}
