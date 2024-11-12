using UnityEngine;
using System.Collections.Generic;

public class StateImageMap<T> : MonoBehaviour
{
    [SerializeField]
    private List<StateImage<T>> stateImages;

    private Dictionary<T, Sprite> stateToImageMap;

    protected virtual void Awake()
    {
        Init();
    }

    private void Init()
    {
        stateToImageMap = new Dictionary<T, Sprite>();
        foreach (var stateImage in stateImages)
        {
            if (!stateToImageMap.ContainsKey(stateImage.state))
            {
                stateToImageMap.Add(stateImage.state, stateImage.image);
            }
        }
    }

    public Sprite GetImageForState(T state)
    {
        Init();
        if (stateToImageMap.ContainsKey(state))
        {
            return stateToImageMap[state];
        }
        else
        {
            Debug.LogWarning($"State {state} �ɑΉ�����摜������܂���B");
            return null;
        }
    }
}
