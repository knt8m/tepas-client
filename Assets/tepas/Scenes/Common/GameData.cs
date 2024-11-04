using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    //�X�e�[�W�i�����
    [SerializeField]
    public List<ClearStage> saveStageData1 = new List<ClearStage>();
    public string data1SavedDate;

    [SerializeField]
    public List<ClearStage> saveStageData2 = new List<ClearStage>();
    public string data2SavedDate;

    [SerializeField]
    public List<ClearStage> saveStageData3 = new List<ClearStage>();
    public string data3SavedDate;

    //�ݒ���
    public int bgmVolume;
    public int seVolume;
    public string mouseLeftClick;

    public GameData()
    {
        saveStageData1 = new List<ClearStage>();
        saveStageData2 = new List<ClearStage>();
        saveStageData3 = new List<ClearStage>();
    }
}
