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
    [LabelText("ó‘Ô"), Required]
    public TabletState state;

    [LabelText("‰æ‘œ"), Required]
    public Sprite image;
}
