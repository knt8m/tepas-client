using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;

public class SettingController : MonoBehaviour
{
    public void onBackButton()
    {
        Scenes.LoadSceneAsync("001_Home");
    }
}
