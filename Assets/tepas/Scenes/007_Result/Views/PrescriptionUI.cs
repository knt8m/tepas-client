using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class PrescriptionUI : MonoBehaviour
{


    [BoxGroup("スコア"), LabelText("トータルスコア"), Required]
    public TextMeshProUGUI totalScoreTxt;

    [BoxGroup("スコア"), LabelText("経過時間"), Required]
    public TextMeshProUGUI timeTxt;

    [BoxGroup("スコア"), LabelText("タブレット数"), Required]
    public TextMeshProUGUI tabletTotalTxt;

    [BoxGroup("スコア"), LabelText("ミス数"), Required]
    public TextMeshProUGUI missCountTxt;

    public ScoreModel scoreModel;

    void Start()
    {
        GameObject scoreObj = GameObject.Find("ScoreModel");
        scoreModel = scoreObj.GetComponent<ScoreModel>();
        timeTxt.text = scoreModel.GetTime();
        tabletTotalTxt.text = Convert.ToString(scoreModel.tabletTotal);
        missCountTxt.text = Convert.ToString(scoreModel.missCount);
        totalScoreTxt.text = Convert.ToString(scoreModel.tabletTotal);
    }

}
