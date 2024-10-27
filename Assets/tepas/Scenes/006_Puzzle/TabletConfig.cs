using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableObject/TabletConfig", fileName = "TabletConfig")]
public class TabletConfig : ScriptableObject
{
    public string Version = "1.0.1";
    public List<TabletImageMap> imageMaps;
}

[Serializable]
public class TabletImageMap
{
    [LabelText("���"), Required]
    public TabletState state;

    [LabelText("�摜"), Required]
    public Sprite image;
}
