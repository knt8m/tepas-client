using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
    [SerializeField]
    MissScoreLoader missScoreLoader;

    [SerializeField]
    TimeScoreLoader timeScoreLoader;

    void Start()
    {
        MissScoreBoard missScoreBoard = missScoreLoader.Load();
        missScoreBoard.Show();
        TimeScoreBoard timeScoreBoard = timeScoreLoader.Load();
        timeScoreBoard.Show();
    }
    public void onRetryButton()
    {
        Scenes.UnloadSceneAsync("006_Puzzle");
        Scenes.UnloadSceneAsync("007_Result");
        Scenes.LoadSceneAsync("006_Puzzle", LoadSceneMode.Additive);
    }

    public void onStageButton()
    {
        Scenes.LoadSceneAsync("005_StageSelect");
    }
}
