using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public int nowDataNo;

    public GameData gameData;

    public ScoreModel scoreModel;
    public int stageNo { get; internal set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadGameData()
    {
        if (ES3.KeyExists("gameData"))
        {
            gameData = ES3.Load<GameData>("gameData");
        }
    }

    public void SaveGameData()
    {
        ES3.Save("gameData", gameData);
    }

    private List<ClearStage> GetClearStages()
    {
        List<ClearStage> clearStages = new List<ClearStage>();
        if (nowDataNo == 1)
        {
            clearStages = gameData.saveStageData1;
        }
        else if (nowDataNo == 2)
        {
            clearStages = gameData.saveStageData2;
        }
        else if (nowDataNo == 3)
        {
            clearStages = gameData.saveStageData3;
        }
        return clearStages;
    }

    private void SetClearStages(List<ClearStage> clearStages)
    {
        if(clearStages == null) return;
        if (nowDataNo == 1)
        {
            gameData.saveStageData1 = clearStages;
        }
        else if (nowDataNo == 2)
        {
            gameData.saveStageData2 = clearStages;
        }
        else if (nowDataNo == 3)
        {
            gameData.saveStageData3 = clearStages;
        }
        return;
    }

    private ClearStage GetClearStage(int stageNo)
    {
        List<ClearStage> clearStages = GetClearStages();
        ClearStage clearStage = null;
        foreach (ClearStage stage in clearStages)
        {
            if (stage.stageNo == stageNo)
            {
                clearStage = stage;
                break;
            }
        }
        return clearStage;
    }

    public void UpdateClearStage(
        int stageNo,
        int highScore,
        bool achievement1 = false,
        bool achievement2 = false,
        bool achievement3 = false
        )
    {
        List<ClearStage> clearStages = GetClearStages();
        int clearHighScore = 0;
        bool hasStage = false;
        foreach (ClearStage stage in clearStages)
        {
            if (stage.stageNo == stageNo)
            {
                hasStage = true;
                clearHighScore = stage.highScore;
                break;
            }
        }
        if(hasStage)
        {
            if(clearHighScore < highScore)
            {
                foreach (ClearStage stage in clearStages)
                {
                    if (stage.stageNo == stageNo)
                    {
                        stage.highScore = highScore;
                        stage.achievement1 = achievement1;
                        stage.achievement2 = achievement2;
                        stage.achievement3 = achievement3;
                        break;
                    }
                }
            }
        }
        else
        {
            ClearStage stage = new ClearStage();
            stage.stageNo = stageNo;
            stage.highScore = highScore;
            clearStages.Add(stage);
        }
        clearStages = clearStages.OrderBy(stage => stage.stageNo).ToList();
        SetClearStages(clearStages);
        //セーブ日時の更新
        if (nowDataNo == 1)
        {
            gameData.data1SavedDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }
        else if (nowDataNo == 2)
        {
            gameData.data2SavedDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }
        else if (nowDataNo == 3)
        {
            gameData.data3SavedDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }
        SaveGameData();
    }
}
