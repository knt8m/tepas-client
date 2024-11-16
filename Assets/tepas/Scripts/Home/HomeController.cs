using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour
{
    public bool firstInit = false;
    SoundManager soundManager;

    string descriptionUrl = "http://yasoukyoku.com/files/tepas_manual_jp.pdf";

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.LoadBgmVolume();
        soundManager.LoadBgmMute();
        soundManager.LoadSeVolume();
        soundManager.LoadSeMute();
        //GameObject.Find("ScoreModel").GetComponent<ScoreModel>();
        //GameObject.Find("GameManager").GetComponent<GameManager>().scoreModel = GameObject.Find("ScoreModel").GetComponent<ScoreModel>();
    }
    public void OnNewGameButton()
    {
        soundManager.PlaySe("Select_01");
        bool isEmptyData = false;
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager.gameData.data1SavedDate == null || gameManager.gameData.data1SavedDate.Length == 0)
        {
            isEmptyData = true;
            gameManager.nowDataNo = 1;
        }
        else if (gameManager.gameData.data2SavedDate == null || gameManager.gameData.data2SavedDate.Length == 0)
        {
            isEmptyData = true;
            gameManager.nowDataNo = 2;
        }
        else if (gameManager.gameData.data3SavedDate == null || gameManager.gameData.data3SavedDate.Length == 0)
        {
            isEmptyData = true;
            gameManager.nowDataNo = 3;
        }
        if (isEmptyData)
        {
            Scenes.LoadSceneAsync("005_StageSelect");
            return;
        }
        //セーブデータがいっぱいならつづきから
        Scenes.LoadSceneAsync("002_Load");
    }

    public void OnLoadGameButton()
    {
        soundManager.PlaySe("Select_01");
        Scenes.LoadSceneAsync("002_Load");
    }

    public void OnSettingGameButton()
    {
        soundManager.PlaySe("Select_01");
        Scenes.LoadSceneAsync("003_Setting");
    }

    public void OnExtraButton()
    {
        soundManager.PlaySe("Select_01");
        Application.OpenURL(descriptionUrl);
        //Scenes.LoadSceneAsync("004_Extra");
    }
}
