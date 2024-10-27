using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [BoxGroup("スコア"), LabelText("経過時間"), Required]
    public TextMeshProUGUI timerText; 

    [BoxGroup("スコア"), LabelText("正解タブレット数"), Required]
    public TextMeshProUGUI tabletTotalText;

    [BoxGroup("スコア"), LabelText("ミス数"), Required]
    public TextMeshProUGUI missCountText; //

    public ScoreModel scoreModel;

    private float time;
    [SerializeField]
    [LabelText("描画頻度")]
    private float interval = 0.1f;

    void Start()
    {
        GameObject scoreObj = GameObject.Find("ScoreModel");
        scoreModel = scoreObj.GetComponent<ScoreModel>();
    }

    void Update()
    {
        timerText.text = scoreModel.GetTime();
        tabletTotalText.text = Convert.ToString(scoreModel.tabletTotal);
        missCountText.text = Convert.ToString(scoreModel.missCount);
        /*
        time += Time.deltaTime;
        if (time >= interval)
        {
            time = 0f;
            Draw();
        }
        */
    }
}
