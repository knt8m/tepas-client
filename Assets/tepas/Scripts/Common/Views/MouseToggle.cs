using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseToggle : MonoBehaviour
{
    public GameManager gameManager;

    public Image leftClick;

    public Image rightClick;

    public Sprite blueTablet;

    public Sprite blackTablet;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        blueTablet = Resources.Load<Sprite>("Visual/Misc/blue_tablet");
        blackTablet = Resources.Load<Sprite>("Visual/Misc/black_tablet");
        LoadLeftClick();
    }

    public void ChangeLeftClick()
    {
        if (leftClick.sprite.name == "blue_tablet")
        {
            leftClick.sprite = blackTablet;
            rightClick.sprite = blueTablet;
        }
        else
        {
            leftClick.sprite = blueTablet;
            rightClick.sprite = blackTablet;
        }
        SaveLeftClick(leftClick.sprite.name);
    }

    public void SaveLeftClick(string spriteName)
    {
        gameManager.gameData.mouseLeftClick = spriteName;
        gameManager.SaveGameData();
    }

    public void LoadLeftClick()
    {
        if (gameManager.gameData.mouseLeftClick == "black_tablet")
        {
            leftClick.sprite = blackTablet;
            rightClick.sprite = blueTablet;
        }
        else
        {
            leftClick.sprite = blueTablet;
            rightClick.sprite = blackTablet;
        }
    }


}
