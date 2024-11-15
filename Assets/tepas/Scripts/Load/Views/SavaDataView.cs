using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using System;

public class SavaDataView : MonoBehaviour
{
    [SerializeField]
    [BoxGroup("セーブデータ"), LabelText("データ番号"), Required]
    public int dataNo;

    [BoxGroup("セーブデータ"), LabelText("ステージNo"), Required]
    public TextMeshProUGUI stageNo;

    [BoxGroup("セーブデータ"), LabelText("日付"), Required]
    public TextMeshProUGUI date;

    SaveDataModel model;

    bool updated = false;


    void Start()
    {
        stageNo.text = "";
        date.text = "";
        if (dataNo == 1)
        {
            model = GameObject.Find("/Models/SaveData1").GetComponent<SaveDataModel>();
        }
        else if (dataNo == 2)
        {
            model = GameObject.Find("/Models/SaveData2").GetComponent<SaveDataModel>();
        }
        else if (dataNo == 3)
        {
            model = GameObject.Find("/Models/SaveData3").GetComponent<SaveDataModel>();
        }
    }

    void Update()
    {
        if (updated) return;
        updated = true;
        stageNo.text = Convert.ToString(model.stageNo);
        date.text = Convert.ToString(model.date);
        
    }
}
