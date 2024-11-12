using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [BoxGroup("�X�R�A"), LabelText("�o�ߎ���"), Required]
    public TextMeshProUGUI timerText; 

    [BoxGroup("�X�R�A"), LabelText("�����^�u���b�g�c��"), Required]
    public TextMeshProUGUI tabletRemainText;

    [BoxGroup("�X�R�A"), LabelText("�~�X��"), Required]
    public TextMeshProUGUI missCountText; //

    public ScoreModel scoreModel;

    private float time;
    [SerializeField]
    [LabelText("�`��p�x")]
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
