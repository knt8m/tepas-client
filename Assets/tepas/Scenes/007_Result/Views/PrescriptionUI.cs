using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class PrescriptionUI : MonoBehaviour
{


    [BoxGroup("�X�R�A"), LabelText("�g�[�^���X�R�A"), Required]
    public TextMeshProUGUI totalScoreTxt;

    [BoxGroup("�X�R�A"), LabelText("�o�ߎ���"), Required]
    public TextMeshProUGUI timeTxt;

    [BoxGroup("�X�R�A"), LabelText("�^�u���b�g��"), Required]
    public TextMeshProUGUI tabletTotalTxt;

    [BoxGroup("�X�R�A"), LabelText("�~�X��"), Required]
    public TextMeshProUGUI missCountTxt;

    public ScoreModel scoreModel;

    void Start()
    {
        scoreModel = GameObject.Find("GameManager").GetComponent<GameManager>().scoreModel;
        timeTxt.text = scoreModel.GetTime();
        tabletTotalTxt.text = Convert.ToString(scoreModel.tabletTotal);
        missCountTxt.text = Convert.ToString(scoreModel.missCount);
    }

    void Update()
    {
        totalScoreTxt.text = Convert.ToString(scoreModel.totalScore);
    }
}
