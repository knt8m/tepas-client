using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public enum TabletPhase
{
    Current,
    Answer
}


public class TabletEditorUI : MonoBehaviour, IPointerClickHandler
{
    [BoxGroup("状態"), LabelText("現在の状態"), Required]
    public TabletState currentState;

    [BoxGroup("状態"), LabelText("回答の状態"), Required]
    public TabletState answerState;

    [BoxGroup("状態"), LabelText("ヒントの数字")]
    public int hintNum;

    public int row;

    public int column;

    public TabletPhase status = TabletPhase.Current;

    private TabletConfig tabletConfig;

    private string tabletConfigPath = "Data/TabletConfig";

    void Start()
    {
        Init();
    }

    public void Init()
    {
        tabletConfig = Resources.Load<TabletConfig>(tabletConfigPath);
        Debug.Log(tabletConfig.Version);
    }


    public void ApplyCurrentState()
    {
        UpdateImage(currentState);
        UpdateText(currentState);
    }

    public void ApplyAnswerState()
    {
        UpdateImage(answerState);
        UpdateText(answerState);
    }

    private void UpdateImage(TabletState state)
    {
        Image image = GetComponentInChildren<Image>();
        if (image == null) return;
        if (state == TabletState.None)
        {
            image.sprite = null;
            image.color = new Color32(255, 255, 255, 0);
            return;
        }
        Debug.Log(state);
        List<TabletImageMap> imageMaps = tabletConfig.imageMaps;
        if (imageMaps == null) return;
        TabletImageMap imageMap = imageMaps.Find(x => x.state == state);
        if (imageMap == null || imageMap.image == null) return;
        image.sprite = imageMap.image;
    }

    private void UpdateText(TabletState state)
    {
        TextMeshProUGUI hintText = GetComponentInChildren<TextMeshProUGUI>();
        if (state == TabletState.Hint)
        {
            hintText.text = hintNum.ToString();
        }
        else
        {
            hintText.text = "";
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Debug.Log(pointerEventData.button);
        //Debug.Log(pointerEventData.hovered);
        //Debug.Log(pointerEventData.position);
        //Debug.Log(pointerEventData.pressEventCamera);
        switch (pointerEventData.button)
        {
            case PointerEventData.InputButton.Left:
                Debug.Log("Left Click");
                if(status == TabletPhase.Current)
                {
                    if (currentState == TabletState.NoneEditor)
                    {
                        currentState = TabletState.Unknown;
                    }
                    else if (currentState == TabletState.Unknown)
                    {
                        currentState = TabletState.Dose;
                    }
                    else if (currentState == TabletState.Dose)
                    {
                        currentState = TabletState.Hint;
                    }
                    else if (currentState == TabletState.Hint)
                    {
                        currentState = TabletState.NoneEditor;
                    }
                    hintNum = 0;
                    UpdateImage(currentState);
                    UpdateText(currentState);
                }
                else if (status == TabletPhase.Answer)
                {
                    if (answerState == TabletState.NoneEditor)
                    {
                        answerState = TabletState.Unknown;
                    }
                    else if (answerState == TabletState.Unknown)
                    {
                        answerState = TabletState.Dose;
                    }
                    else if (answerState == TabletState.Dose)
                    {
                        answerState = TabletState.Hint;
                    }
                    else if (answerState == TabletState.Hint)
                    {
                        answerState = TabletState.NoneEditor;
                    }
                    hintNum = 0;
                    UpdateImage(answerState);
                    UpdateText(answerState);
                }
                break;
            case PointerEventData.InputButton.Right:
                Debug.Log("Right Click");
                if (status == TabletPhase.Current)
                {
                    Debug.Log("Current");
                    Debug.Log(currentState);
                    if (currentState == TabletState.Hint && hintNum == 0)
                    {
                        currentState = TabletState.Hint;
                        hintNum = 1;
                    }
                    else if (currentState == TabletState.Hint && hintNum == 1)
                    {
                        currentState = TabletState.Hint;
                        hintNum = 2;
                    }
                    else if (currentState == TabletState.Hint && hintNum == 2)
                    {
                        currentState = TabletState.Hint;
                        hintNum = 3;
                    }
                    else if (currentState == TabletState.Hint && hintNum == 3)
                    {
                        currentState = TabletState.Hint;
                        hintNum = 4;
                    }
                    else if (currentState == TabletState.Hint && hintNum == 4)
                    {
                        currentState = TabletState.Hint;
                        hintNum = 5;
                    }
                    else if (currentState == TabletState.Hint && hintNum == 5)
                    {
                        currentState = TabletState.Hint;
                        hintNum = 6;
                    }
                    else if (currentState == TabletState.Hint && hintNum == 6)
                    {
                        currentState = TabletState.Hint;
                        hintNum = 7;
                    }
                    else
                    {
                        currentState = TabletState.Hint;
                        hintNum = 0;
                    }
                    UpdateImage(currentState);
                    UpdateText(currentState);
                }
                else if (status == TabletPhase.Answer)
                {
                    if (answerState == TabletState.Hint && hintNum == 0)
                    {
                        answerState = TabletState.Hint;
                        hintNum = 1;
                    }
                    else if (answerState == TabletState.Hint && hintNum == 1)
                    {
                        answerState = TabletState.Hint;
                        hintNum = 2;
                    }
                    else if (answerState == TabletState.Hint && hintNum == 2)
                    {
                        answerState = TabletState.Hint;
                        hintNum = 3;
                    }
                    else if (answerState == TabletState.Hint && hintNum == 3)
                    {
                        answerState = TabletState.Hint;
                        hintNum = 4;
                    }
                    else if (answerState == TabletState.Hint && hintNum == 4)
                    {
                        answerState = TabletState.Hint;
                        hintNum = 5;
                    }
                    else if (answerState == TabletState.Hint && hintNum == 5)
                    {
                        answerState = TabletState.Hint;
                        hintNum = 6;
                    }
                    else if (answerState == TabletState.Hint && hintNum == 6)
                    {
                        answerState = TabletState.Hint;
                        hintNum = 7;
                    }
                    else
                    {
                        answerState = TabletState.Hint;
                        hintNum = 0;
                    }
                    UpdateImage(answerState);
                    UpdateText(answerState);
                }
                break;
            case PointerEventData.InputButton.Middle:

                break;
        }
        
        
    }


    [BoxGroup("デバッグ"), Button("タブレット状態変更", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void DebugApplyState(TabletState state, int hintNum = 0)
    {
        this.hintNum = hintNum;
        UpdateImage(state);
        UpdateText(state);
    }

}
