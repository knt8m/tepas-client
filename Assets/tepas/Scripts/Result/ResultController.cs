using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    MissScoreLoader missScoreLoader;

    [SerializeField]
    TimeScoreLoader timeScoreLoader;

    [SerializeField]
    TrophyLoader trophyLoader;

    [SerializeField]
    public Sprite trophyNoneImg;

    [SerializeField]
    public Sprite trophyBlueImg;

    [SerializeField]
    public Sprite trophyRedImg;


    public int score;

    public PrescriptionUI prescriptionUi;
    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        MissScoreBoard missScoreBoard = missScoreLoader.Load();
        //missScoreBoard.Show();
        TimeScoreBoard timeScoreBoard = timeScoreLoader.Load();
        //timeScoreBoard.Show();
        TrophyBoard trophyBoard = trophyLoader.Load();
        trophyBoard.Show();
        ScoreModel scoreModel;
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreModel = gameManager.scoreModel;
        float time = scoreModel.timer;
        int tablet = scoreModel.tabletTotal;
        int miss = scoreModel.missCount;
        // スコア
        int timePoint;
        int tabletPoint;
        int missPoint;
        // トロフィー取得リスト
        List<Trophy> trophyGetList = new List<Trophy>();


        //スコアボードに従った処理
        timePoint = 0;
        foreach (TimeScore score in timeScoreBoard.timeScores)
        {
            if (score.startTime <= time && score.endTime >= time)
            {
                timePoint = score.score;
                Debug.Log($"time {score.startTime} - {score.endTime}, score: {score.score}");
                break;
            }
        }
        missPoint = 0;
        foreach (MissScore score in missScoreBoard.missScores)
        {
            if (score.startMiss <= miss && score.endMiss >= miss)
            {
                missPoint = score.score;
                Debug.Log($"miss {score.startMiss} - {score.endMiss}, score: {score.score}");
                break;
            }
        }
        tabletPoint = tablet * 50;
        score = timePoint + tabletPoint + missPoint;
        scoreModel.SetTotalScore(score);
        scoreModel.totalScoreProcess = score + " = time:" + timePoint + " + " + "tablet:" + tabletPoint + " + " + "miss:" + missPoint;
        gameManager.UpdateClearStage(gameManager.stageNo, score);


        //トロフィー取得
        foreach (Trophy trophy in trophyBoard.trophies)
        {
            if(gameManager.stageNo == trophy.stage)
            {
                if (trophy.borderScore <= score)
                {
                    trophyGetList.Add(trophy);
                }
            }
        }
        //昇順に並び替え
        trophyGetList = trophyGetList.OrderBy(trophy => trophy.no).ToList();
        //トロフィー設定
        int index = 1;
        Image trophy1 = GameObject.Find("Trophy1").GetComponent<Image>();
        Image trophy2 = GameObject.Find("Trophy2").GetComponent<Image>();
        Image trophy3 = GameObject.Find("Trophy3").GetComponent<Image>();
        foreach (Trophy trophy in trophyGetList)
        {
            if(index == 1)
            {
                if (trophy.color == "blue")
                {
                    trophy1.sprite = trophyBlueImg;
                }
                else if (trophy.color == "red")
                {
                    trophy1.sprite = trophyRedImg;
                }
                gameManager.UpdateOwnTrophy(trophy.no, trophy.color);
            }
            else if (index == 2)
            {
                if (trophy.color == "blue")
                {
                    trophy2.sprite = trophyBlueImg;
                }
                else if (trophy.color == "red")
                {
                    trophy2.sprite = trophyRedImg;
                }
                gameManager.UpdateOwnTrophy(trophy.no, trophy.color);
            }
            else if (index == 3)
            {
                if (trophy.color == "blue")
                {
                    trophy3.sprite = trophyBlueImg;
                }
                else if (trophy.color == "red")
                {
                    trophy3.sprite = trophyRedImg;
                }
                gameManager.UpdateOwnTrophy(trophy.no, trophy.color);
            }
            index++;
        }

        gameManager.SaveGameData();
        //Debug.Log(JsonUtility.ToJson(gameManager.gameData));
    }

    public void onRetryButton()
    {
        soundManager.PlaySe("Decision_06");
        Scenes.LoadSceneAsync("006_Puzzle");
    }

    public void onStageButton()
    {
        soundManager.PlaySe("Select_05");
        Scenes.LoadSceneAsync("005_StageSelect");
    }

    public void onNextButton()
    {
        soundManager.PlaySe("Select_05");

        int nowStageNo = GameObject.Find("GameManager").GetComponent<GameManager>().stageNo;
        if (nowStageNo <= 0 ||  nowStageNo == 15)
        {
            Scenes.LoadSceneAsync("005_StageSelect");
            return;
        }
        else
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().stageNo = nowStageNo + 1;
            Scenes.LoadSceneAsync("006_Puzzle");
        }
    }
}
