using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoadController : MonoBehaviour
{

    public SaveDataModel saveData1;

    public SaveDataModel saveData2;

    public SaveDataModel saveData3;

    public void onBackButton()
    {
        Scenes.LoadSceneAsync("001_Home");
    }

    public void onLoadButton(int dataNo)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.nowDataNo = dataNo;
        Scenes.LoadSceneAsync("005_StageSelect");
    }

    void Start()
    {
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
