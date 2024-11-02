using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [BoxGroup("スコア"), LabelText("経過時間"), Required]
    public TextMeshProUGUI timerText; 

    [BoxGroup("スコア"), LabelText("正解タブレット残数"), Required]
    public TextMeshProUGUI tabletRemainText;

    [BoxGroup("スコア"), LabelText("ミス数"), Required]
    public TextMeshProUGUI missCountText; //

    public ScoreModel scoreModel;

    private float time;
    [SerializeField]
    [LabelText("描画頻度")]
    private float interval = 0.1f;

    void Start()
    {
        scoreModel = GameObject.Find("GameManager").GetComponent<GameManager>().scoreModel;
    }

    void Update()
    {
        timerText.text = scoreModel.GetTime();
        tabletRemainText.text = Convert.ToString(scoreModel.tabletRemain);
        missCountText.text = Convert.ToString(scoreModel.missCount);
    }
}
