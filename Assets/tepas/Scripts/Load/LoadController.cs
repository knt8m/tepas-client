using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class LoadController : MonoBehaviour
{

    public SaveDataModel saveData1;

    public SaveDataModel saveData2;

    public SaveDataModel saveData3;

    [SerializeField]
    public Sprite trophyNoneImg;

    [SerializeField]
    public Sprite trophyBlueImg;

    [SerializeField]
    public Sprite trophyRedImg;

    SoundManager soundManager;

    public void onBackButton()
    {
        soundManager.PlaySe("Select_05");
        Scenes.LoadSceneAsync("001_Home");
    }

    public void onLoadButton(int dataNo)
    {
        soundManager.PlaySe("Decision_01");
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.nowDataNo = dataNo;
        Scenes.LoadSceneAsync("005_StageSelect");
    }

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        DefaultData();
    }

    private void DefaultData()
    {

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.LoadGameData();
        //データ1
        int index = 1;
        bool isSaveData = false;
        foreach (ClearStage stage in gameManager.gameData.saveStageData1)
        {
            isSaveData = true;
            if (stage.stageNo != index)
            {
                break;
            }
            index++;
        }
        int data1maxStage = index - 1;
        GameObject NoData = GameObject.Find("/Canvas/Data1/NoData").gameObject;
        GameObject SaveData = GameObject.Find("/Canvas/Data1/SaveData").gameObject;
        if (isSaveData)
        {
            SaveData.SetActive(true);
            NoData.SetActive(false);
            //トロフィーデータ
            int trophyIndex = 1;
            List<OwnTrophy> ownTrophies = gameManager.gameData.ownTrophyData1;
            Image trophyImg = null;
            foreach (OwnTrophy ownTrophy in ownTrophies)
            {
                trophyImg = GameObject.Find("Data1/SaveData/trophy/" + ownTrophy.trophyNo).GetComponent<Image>();
                if (ownTrophy.color == "none")
                {
                    trophyImg.sprite = trophyNoneImg;
                }
                else if (ownTrophy.color == "blue")
                {
                    trophyImg.sprite = trophyBlueImg;
                }
                else if (ownTrophy.color == "red")
                {
                    trophyImg.sprite = trophyRedImg;
                }
                trophyIndex++;
            }
        }
        else
        {
            SaveData.SetActive(false);
            NoData.SetActive(true);
        }

        //データ2
        index = 1;
        isSaveData = false;
        foreach (ClearStage stage in gameManager.gameData.saveStageData2)
        {
            isSaveData = true;
            if (stage.stageNo != index)
            {
                break;
            }
            index++;
        }
        int data2maxStage = index - 1;
        NoData = GameObject.Find("/Canvas/Data2/NoData").gameObject;
        SaveData = GameObject.Find("/Canvas/Data2/SaveData").gameObject;
        if (isSaveData)
        {
            SaveData.SetActive(true);
            NoData.SetActive(false);
            //トロフィーデータ
            int trophyIndex = 1;
            List<OwnTrophy> ownTrophies = gameManager.gameData.ownTrophyData2;
            Image trophyImg = null;
            foreach (OwnTrophy ownTrophy in ownTrophies)
            {
                trophyImg = GameObject.Find("Data2/SaveData/trophy/" + ownTrophy.trophyNo).GetComponent<Image>();
                if (ownTrophy.color == "none")
                {
                    trophyImg.sprite = trophyNoneImg;
                }
                else if (ownTrophy.color == "blue")
                {
                    trophyImg.sprite = trophyBlueImg;
                }
                else if (ownTrophy.color == "red")
                {
                    trophyImg.sprite = trophyRedImg;
                }
                trophyIndex++;
            }
        }
        else
        {
            SaveData.SetActive(false);
            NoData.SetActive(true);
        }

        //データ3
        index = 1;
        isSaveData = false;
        foreach (ClearStage stage in gameManager.gameData.saveStageData3)
        {
            isSaveData = true;
            if (stage.stageNo != index)
            {
                break;
            }
            index++;
        }
        int data3maxStage = index - 1;
        NoData = GameObject.Find("/Canvas/Data3/NoData").gameObject;
        SaveData = GameObject.Find("/Canvas/Data3/SaveData").gameObject;
        if (isSaveData)
        {
            SaveData.SetActive(true);
            NoData.SetActive(false);
            //トロフィーデータ
            int trophyIndex = 1;
            List<OwnTrophy> ownTrophies = gameManager.gameData.ownTrophyData3;
            Image trophyImg = null;
            foreach (OwnTrophy ownTrophy in ownTrophies)
            {
                trophyImg = GameObject.Find("Data3/SaveData/trophy/" + ownTrophy.trophyNo).GetComponent<Image>();
                if (ownTrophy.color == "none")
                {
                    trophyImg.sprite = trophyNoneImg;
                }
                else if (ownTrophy.color == "blue")
                {
                    trophyImg.sprite = trophyBlueImg;
                }
                else if (ownTrophy.color == "red")
                {
                    trophyImg.sprite = trophyRedImg;
                }
                trophyIndex++;
            }
        }
        else
        {
            SaveData.SetActive(false);
            NoData.SetActive(true);
        }

        //モデルデータ登録処理
        saveData1.stageNo = data1maxStage;
        saveData2.stageNo = data2maxStage;
        saveData3.stageNo = data3maxStage;
        saveData1.date = gameManager.gameData.data1SavedDate;
        saveData2.date = gameManager.gameData.data2SavedDate;
        saveData3.date = gameManager.gameData.data3SavedDate;
    }
}
