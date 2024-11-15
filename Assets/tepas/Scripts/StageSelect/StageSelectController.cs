using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class StageSelectController : MonoBehaviour
{
    public int stageNo;

    public GameObject Normal;

    public GameObject Hard;

    [SerializeField]
    public Sprite trophyNoneImg;

    [SerializeField]
    public Sprite trophyBlueImg;

    [SerializeField]
    public Sprite trophyRedImg;

    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        List<OwnTrophy> ownTrophies = null;
        if (gameManager.nowDataNo == 1)
        {
            ownTrophies = gameManager.gameData.ownTrophyData1;
        }
        else if (gameManager.nowDataNo == 2)
        {
            ownTrophies = gameManager.gameData.ownTrophyData2;
        }
        else if (gameManager.nowDataNo == 3)
        {
            ownTrophies = gameManager.gameData.ownTrophyData3;
        }
        Image trophyImg = null;
        Normal.SetActive(true);
        Hard.SetActive(true);
        foreach (OwnTrophy ownTrophy in ownTrophies)
        {
            trophyImg = GameObject.Find("trophy" + ownTrophy.trophyNo).GetComponent<Image>();
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
        }
        Hard.SetActive(false);
    }


    public void onBackButton()
    {
        soundManager.PlaySe("Select_05");
        Scenes.LoadSceneAsync("001_Home");
    }

    public void onStageButton(int stageNo)
    {
        soundManager.PlaySe("Decision_01");
        this.stageNo = stageNo;
        GameObject.Find("GameManager").GetComponent<GameManager>().stageNo = stageNo;
        Scenes.LoadSceneAsync("006_Puzzle");
    }
}
