using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    public void onBackButton()
    {
        Scenes.LoadSceneAsync("001_Home");
    }

    public void onLoadButton()
    {
        Scenes.LoadSceneAsync("005_StageSelect");
    }
}
