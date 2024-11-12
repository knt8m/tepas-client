using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class InGameController : MonoBehaviour
{
    [BoxGroup("オブジェクト"), LabelText("残数"), Required] 
    public Remain remain;


    [BoxGroup("オブジェクト"), LabelText("ミス数"), Required] 
    public Miss miss;

    void Start()
    {
        remain.Default(10);
        miss.Default(10);
    }

    void Update()
    {
        
    }

    [BoxGroup("デバッグ"), Button("現在のスコア", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    private void GetCurrentScore()
    {
        Debug.Log("残数: " + remain.text.text);
        Debug.Log("ミス数: " + miss.text.text);
    }
}
