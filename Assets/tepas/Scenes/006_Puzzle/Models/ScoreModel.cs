using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreModel : MonoBehaviour
{
    public static ScoreModel instance;
    public float timer;
    public int tabletTotal;
    public int tabletRemain;
    public int missCount;
    public int totalScore;
    public string totalScoreProcess;

    void Awake()
    {
        CheckInstance();
    }

    void Start()
    {
        Init();
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        timer = 0f;
        tabletTotal = 0;
        tabletRemain = 0;
        missCount = 0;
    }

    public void Default(int tabletTotal = 0, int missCount = 0)
    {
        this.tabletTotal = tabletTotal;
        this.tabletRemain = tabletTotal;
        this.missCount = missCount;
    }
    public void SetTime(float time = 0f)
    {
        this.timer = time;
    }

    public void SetTotalScore(int totalScore = 0)
    {
        this.totalScore = totalScore;
    }

    public string GetTime()
    {
        return string.Format("{0:D2}:{1:D2}",
        (int)((timer % 3600) / 60),
        (int)(timer % 60));
    }

    public void Miss()
    {
        ++this.missCount;
    }

    public void Correct()
    {
        --this.tabletRemain;
    }
}
