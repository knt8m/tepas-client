using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    void Start()
    {
        //GameObject.Find("ScoreModel").GetComponent<ScoreModel>();
        //GameObject.Find("GameManager").GetComponent<GameManager>().scoreModel = GameObject.Find("ScoreModel").GetComponent<ScoreModel>();
    }
    public void OnNewGameButton()
    {
        //GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //gameManager.LoadGameData();
        Scenes.LoadSceneAsync("005_StageSelect");
    }

    public void OnLoadGameButton()
    {
        Scenes.LoadSceneAsync("002_Load");
    }

    public void OnSettingGameButton()
    {
        Scenes.LoadSceneAsync("003_Setting");
    }

    public void OnExtraButton()
    {
        Scenes.LoadSceneAsync("004_Extra");
    }
}
