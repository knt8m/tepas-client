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
            gameData = new GameData();
            DontDestroyOnLoad(gameObject);
            LoadGameData();
            InitOwnTrophy();
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

    public void GetOwnTrophy()
    {

    }

    public void InitOwnTrophy()
    {
        List<OwnTrophy> ownTrophies = gameData.ownTrophyData1;
        OwnTrophy trophy = null;
        if (ownTrophies == null || ownTrophies.Count == 0)
        {
            ownTrophies = new List<OwnTrophy>();
            for (int i = 1; i < 46; i++)
            {
                trophy = new OwnTrophy();
                trophy.trophyNo = i;
                trophy.color = "none";
                ownTrophies.Add(trophy);
            }
            gameData.ownTrophyData1 = ownTrophies;
        }

        ownTrophies = gameData.ownTrophyData2;
        if (ownTrophies == null || ownTrophies.Count == 0)
        {
            ownTrophies = new List<OwnTrophy>();
            for (int i = 1; i < 46; i++)
            {
                trophy = new OwnTrophy();
                trophy.trophyNo = i;
                trophy.color = "none";
                ownTrophies.Add(trophy);
            }
            gameData.ownTrophyData2 = ownTrophies;
        }

        ownTrophies = gameData.ownTrophyData3;
        if (ownTrophies == null || ownTrophies.Count == 0)
        {
            ownTrophies = new List<OwnTrophy>();
            for (int i = 1; i < 46; i++)
            {
                trophy = new OwnTrophy();
                trophy.trophyNo = i;
                trophy.color = "none";
                ownTrophies.Add(trophy);
            }
            gameData.ownTrophyData3 = ownTrophies;
        }

#if UNITY_EDITOR
        foreach (OwnTrophy ownTrophy in ownTrophies)
        {
            Debug.Log("-----------------");
            Debug.Log(ownTrophy.trophyNo);
            Debug.Log(ownTrophy.color);
        }
#endif
        SaveGameData();
    }

    public void UpdateOwnTrophy(int trophyNo, string color)
    {
        if(nowDataNo == 1)
        {           
            OwnTrophy trophyToUpdate = gameData.ownTrophyData1.FirstOrDefault(t => t.trophyNo == trophyNo);
            if (trophyToUpdate != null)
            {
                trophyToUpdate.color = color;
            }
            else
            {
#if UNITY_EDITOR
                Console.WriteLine($"Trophy {trophyNo} not found.");
#endif
            }
        }
        else if (nowDataNo == 2)
        {
            OwnTrophy trophyToUpdate = gameData.ownTrophyData2.FirstOrDefault(t => t.trophyNo == trophyNo);
            if (trophyToUpdate != null)
            {
                trophyToUpdate.color = color;
            }
            else
            {
#if UNITY_EDITOR
                Console.WriteLine($"Trophy {trophyNo} not found.");
#endif
            }
        }
        else if (nowDataNo == 3)
        {
            OwnTrophy trophyToUpdate = gameData.ownTrophyData3.FirstOrDefault(t => t.trophyNo == trophyNo);
            if (trophyToUpdate != null)
            {
                trophyToUpdate.color = color;
            }
            else
            {
#if UNITY_EDITOR
                Console.WriteLine($"Trophy {trophyNo} not found.");
#endif
            }
        }


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
