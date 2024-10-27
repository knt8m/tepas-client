using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class InGameController : MonoBehaviour
{
    [BoxGroup("�I�u�W�F�N�g"), LabelText("�c��"), Required] 
    public Remain remain;


    [BoxGroup("�I�u�W�F�N�g"), LabelText("�~�X��"), Required] 
    public Miss miss;

    void Start()
    {
        remain.Default(10);
        miss.Default(10);
    }

    void Update()
    {
        
    }

    [BoxGroup("�f�o�b�O"), Button("���݂̃X�R�A", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    private void GetCurrentScore()
    {
        Debug.Log("�c��: " + remain.text.text);
        Debug.Log("�~�X��: " + miss.text.text);
    }
}
