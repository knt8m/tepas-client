using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;

public class SettingController : MonoBehaviour
{
    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.LoadBgmVolume();
        soundManager.LoadBgmMute();
        soundManager.LoadSeVolume();
        soundManager.LoadSeMute();
    }

    public void onBackButton()
    {
        soundManager.SaveBgmVolume();
        soundManager.SaveBgmMute();
        soundManager.SaveSeVolume();
        soundManager.SaveSeMute();
        Scenes.LoadSceneAsync("001_Home");
    }
}
