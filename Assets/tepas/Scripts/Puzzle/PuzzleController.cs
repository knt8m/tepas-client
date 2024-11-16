using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using System;

public class PuzzleController : MonoBehaviour
{
    [SerializeField]
    StageLoader stageLoader;

    [SerializeField]
    Stage stage;

    [SerializeField]
    public Score score;

    public ScoreModel scoreModel;
    private float time;
    private bool isResultScene = false;
    SoundManager soundManager;

    void Start()
    {
        int stageNo = 1;
        stageNo = GameObject.Find("GameManager").GetComponent<GameManager>().stageNo;
        stageLoader.LoadStageData(stageNo);
        stage = stageLoader.GetStageData();
        this.scoreModel = GameObject.Find("GameManager").GetComponent<GameManager>().scoreModel;
        scoreModel.Default(stage.Remain, 0);
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if (isResultScene) return;
        //タイマー処理
        time += Time.deltaTime;
        scoreModel.SetTime(time);
        if (time > 3599)
        {
            scoreModel.SetTime(3599);
        }
        //終了条件
        if (scoreModel.tabletRemain <= 0)
        {

            soundManager.PlaySe("Decision_04");
            isResultScene = true;
            Scenes.LoadSceneAsync("007_Result");
        }
    }

}
