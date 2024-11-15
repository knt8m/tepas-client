using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject menuBackground;
    public GameObject menuWindow;

    public TextMeshProUGUI stageText;
    SoundManager soundManager;

    void Start()
    {
        int stageNo = GameObject.Find("GameManager").GetComponent<GameManager>().stageNo;
        stageText.text = "Stage" + stageNo;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
    public void ToggleMenu()
    {
        soundManager.PlaySe("Pause_03");
        if (menuWindow.activeSelf)
        {
            menuWindow.SetActive(false);
            menuBackground.SetActive(false);
        }
        else
        {
            menuWindow.SetActive(true);
            menuBackground.SetActive(true);
        }
    }



    public void OnRetryButton()
    {
        Scenes.LoadScene("006_Puzzle");
    }

    public void OnStageSelectButton()
    {
        Scenes.LoadSceneAsync("005_StageSelect");
    }
}
