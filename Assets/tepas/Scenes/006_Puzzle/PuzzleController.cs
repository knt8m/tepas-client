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


    void Start()
    {
        //DontDestroyOnLoad(scoreModel);
        int stageNo = 1;
        //�X�e�[�W�Ǎ�
        stageLoader.LoadStageData(stageNo);
        stage = stageLoader.GetStageData();
        scoreModel.Default(stage.Remain, 0);
    }

    void Update()
    {
        if (isResultScene) return;
        //�^�C�}�[����
        time += Time.deltaTime;
        scoreModel.SetTime(time);
        if (time > 3599)
        {
            scoreModel.SetTime(3599);
        }
        //�I������
        if (scoreModel.tabletTotal <= 0)
        {
            isResultScene = true;
            Scenes.LoadSceneAsync("007_Result", LoadSceneMode.Additive);
        }
    }

}
