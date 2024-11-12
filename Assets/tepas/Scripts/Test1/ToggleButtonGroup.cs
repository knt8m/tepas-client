using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ToggleButtonGroup : MonoBehaviour
{
    [SerializeField] private List<ToggleButton> _toggleButtons = new();

    private ToggleButton _selectedButton;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (var one in _toggleButtons)
        {
            one.OnStateChangedAsObservable().Subscribe(
                state =>
                {
                    if (state == ToggleButton.State.Selected)
                    {
                        if (_selectedButton != null)
                        {
                            _selectedButton.IsManaged = false;
                            _selectedButton.SwitchToggleState();
                        }
                        one.IsManaged = true;
                        _selectedButton = one;
                    }
                }).AddTo(this);
        }
    }

    public void SelectToggleIndex(int index)
    {
        if (0 <= index && _toggleButtons.Count > index)
        {
            _toggleButtons[index].SwitchToggleState();
        }
    }

    public int GetSelectedIndex()
    {
        int index = 0;
        for (var i = 0; i < _toggleButtons.Count; i++)
        {
            if (_toggleButtons[i] == _selectedButton)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
