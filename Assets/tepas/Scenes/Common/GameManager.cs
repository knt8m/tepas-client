using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }


    public ScoreModel scoreModel;
    public int stageNo { get; internal set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
