using UnityEngine;
using AnnulusGames.SceneSystem;
using UnityEngine.SceneManagement;

public class StageSelectController : MonoBehaviour
{
    public int stageNo;

    public void onBackButton()
    {
        Scenes.LoadSceneAsync("001_Home");
    }

    public void onStageButton(int stageNo)
    {
        this.stageNo = stageNo;
        Scenes.LoadSceneAsync("006_Puzzle", LoadSceneMode.Additive);
    }
}
