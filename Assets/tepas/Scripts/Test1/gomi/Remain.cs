using TMPro;
using UnityEngine;

public class Remain : MonoBehaviour
{
    private Sprite image;

    private TextMeshProUGUI title;
    
    [SerializeField]
    public TextMeshProUGUI text;

    public void Default(int value = -1)
    {
        text.text = value.ToString();
    }

    public void Down()
    {
        int value = int.Parse(text.text);
        value--;
        text.text = value.ToString();
    }

    public void Up()
    {
        int value = int.Parse(text.text);
        value++;
        text.text = value.ToString();
    }

}
