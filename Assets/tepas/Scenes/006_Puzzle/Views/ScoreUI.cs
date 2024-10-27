using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    [BoxGroup("�X�R�A"), LabelText("�o�ߎ���"), Required]
    public TextMeshProUGUI timerText; 

    [BoxGroup("�X�R�A"), LabelText("�����^�u���b�g��"), Required]
    public TextMeshProUGUI tabletTotalText;

    [BoxGroup("�X�R�A"), LabelText("�~�X��"), Required]
    public TextMeshProUGUI missCountText; //

    public ScoreModel scoreModel;

    private float time;
    [SerializeField]
    [LabelText("�`��p�x")]
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
