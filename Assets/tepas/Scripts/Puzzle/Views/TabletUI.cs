using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TabletUI : MonoBehaviour, IPointerClickHandler
{
    [BoxGroup("���"), LabelText("���݂̏��"), Required]
    public TabletState currentState;

    [BoxGroup("���"), LabelText("�񓚂̏��"), Required]
    public TabletState answerState;

    [BoxGroup("���"), LabelText("�q���g�̐���")]
    public int hintNum;

    private TabletConfig tabletConfig;
    private string tabletConfigPath = "Data/TabletConfig";

    private ScoreModel scoreModel;
    private GameManager gameManager;

    SoundManager soundManager;

    void Start()
    {
        
        Init();
    }

    public void Init()
    {
        tabletConfig = Resources.Load<TabletConfig>(tabletConfigPath);
        scoreModel = GameObject.Find("GameManager").GetComponent<GameManager>().scoreModel;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
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

    private void Answer()
    {

    }

    private void AnswerState()
    {

    }

    

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        TabletState selectAnswerState;
        switch (pointerEventData.button)
        {
            case PointerEventData.InputButton.Left:
                if (gameManager.gameData.mouseLeftClick == "black_tablet")
                {
                    selectAnswerState = TabletState.Hint;
                }
                else
                {
                    selectAnswerState = TabletState.Dose;
                }
                if (currentState == TabletState.Unknown)
                {
                    if (selectAnswerState == answerState)
                    {
                        if (answerState == TabletState.Dose)
                        {
                            soundManager.PlaySe("Select_01");
                        }
                        else if (answerState == TabletState.Hint)
                        {
                            soundManager.PlaySe("Select_03");
                        }

                        currentState = answerState;
                        UpdateImage(answerState);
                        UpdateText(answerState);
                        scoreModel.Correct();
                    }
                    else
                    {
                        soundManager.PlaySe("Pause_01");
                        scoreModel.Miss();
                    }
                }
                else
                {

                }
                break;
            case PointerEventData.InputButton.Right:
                if (gameManager.gameData.mouseLeftClick == "black_tablet")
                {
                    selectAnswerState = TabletState.Dose;
                }
                else
                {
                    selectAnswerState = TabletState.Hint;
                }
                if (currentState == TabletState.Unknown)
                {
                    if (selectAnswerState == answerState)
                    {
                        if(answerState == TabletState.Dose)
                        {
                            soundManager.PlaySe("Select_01");
                        }
                        else if(answerState == TabletState.Hint)
                        {
                            soundManager.PlaySe("Select_03");
                        }
                        currentState = answerState;
                        UpdateImage(answerState);
                        UpdateText(answerState);
                        scoreModel.Correct();
                    }
                    else
                    {
                        soundManager.PlaySe("Pause_01");
                        scoreModel.Miss();
                    }
                }
                else
                {

                }
                break;
        }
    }


    [BoxGroup("�f�o�b�O"), Button("�^�u���b�g��ԕύX", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void DebugApplyState(TabletState state, int hintNum = 0)
    {
        this.hintNum = hintNum;
        UpdateImage(state);
        UpdateText(state);
    }

}
