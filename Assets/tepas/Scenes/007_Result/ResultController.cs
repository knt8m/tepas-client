using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using System;
using System.Text.Json;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    MissScoreLoader missScoreLoader;

    [SerializeField]
    TimeScoreLoader timeScoreLoader;

    public int score;

    public PrescriptionUI prescriptionUi;

    void Start()
    {
        MissScoreBoard missScoreBoard = missScoreLoader.Load();
        missScoreBoard.Show();
        TimeScoreBoard timeScoreBoard = timeScoreLoader.Load();
        timeScoreBoard.Show();
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
        gameManager.SaveGameData();
        Debug.Log(JsonUtility.ToJson(gameManager.gameData));
    }

    public void onRetryButton()
    {
        Scenes.LoadSceneAsync("006_Puzzle");
    }

    public void onStageButton()
    {
        Scenes.LoadSceneAsync("005_StageSelect");
    }
}
