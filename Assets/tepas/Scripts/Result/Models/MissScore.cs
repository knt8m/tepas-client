using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[Serializable]
public class MissScore
{
    [SerializeField]
    public int startMiss = 0;

    [SerializeField]
    public int endMiss = 0;

    [SerializeField]
    public int score = 0;
}
