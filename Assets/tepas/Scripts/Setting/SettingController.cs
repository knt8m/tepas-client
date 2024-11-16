using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    SoundManager soundManager;

    public Toggle bgmToggle;
    public Toggle seToggle;

    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.LoadBgmVolume();
        soundManager.LoadBgmMute();
        soundManager.LoadSeVolume();
        soundManager.LoadSeMute();

        if (gameManager.gameData.bgmMute)
        {
            bgmToggle.SetIsOnWithoutNotify(true);
        }
        if (gameManager.gameData.seMute)
        {
            seToggle.SetIsOnWithoutNotify(true);
        }

    }

    public void onBackButton()
    {
        soundManager.PlaySe("Select_05");
        soundManager.SaveBgmVolume();
        soundManager.SaveBgmMute();
        soundManager.SaveSeVolume();
        soundManager.SaveSeMute();
        Scenes.LoadSceneAsync("001_Home");
    }
}
