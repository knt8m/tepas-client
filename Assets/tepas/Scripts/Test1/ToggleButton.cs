using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : Button
{
    public enum State
    {
        Default,
        Selected,
    }

    [SerializeField] private GameObject _defaultObject;
    [SerializeField] private GameObject _selectedObject;

    private ReactiveProperty<State> _state = new();
    private Subject<State> _onStateChanged = new();

    public bool IsManaged { get; set; }

    /// <summary>
    /// ステート変更時発火するイベント
    /// </summary>
    public IObservable<State> OnStateChangedAsObservable()
    {
        return _onStateChanged.AsObservable();
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _state.Subscribe(
            state =>
            {
                _defaultObject.SetActive(state == State.Default);
                _selectedObject.SetActive(state == State.Selected);
                _onStateChanged.OnNext(state);
            }).AddTo(this);

        _state.Value = State.Default;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        SwitchToggleState();
    }

    public void SwitchToggleState()
    {
        if (interactable && !IsManaged)
        {
            switch (_state.Value)
            {
                case State.Default:
                    _state.Value = State.Selected;
                    break;
                case State.Selected:
                    _state.Value = State.Default;
                    break;
            }
        }
    }
}
