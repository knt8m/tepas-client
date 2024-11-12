using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

public class EditStageController : MonoBehaviour
{
    [SerializeField]
    GameObject stage;

    [SerializeField]
    Stage stageData;

    [SerializeField]
    TextMeshProUGUI modeText;

    private string editableTabletPath = "Visual/Misc/EditableTablet";

    public int GetRemainNum()
    {
        int count = 0;
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                TabletEditorUI tabletUi = child.gameObject.GetComponent<TabletEditorUI>();
                if(tabletUi.currentState == TabletState.Unknown)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void SaveRemain()
    {
        int remainNum = GetRemainNum();
        stageData.Remain = remainNum;
    }

    public string GetFullPath()
    {
#if !UNITY_EDITOR
        string basePath = Application.persistentDataPath;
#else
        string basePath = Application.dataPath;
#endif
        string dataPath = "tepas/Resources/Data";
        string stageNum = stageData.Number.ToString();
        return $"{basePath}/{dataPath}/Stage{stageNum}.json";
    }

    public bool SaveJson()
    {
        bool result = false;
        string fullPath = GetFullPath();
        string json = JsonUtility.ToJson(stageData);
        Debug.Log("SaveSettings: " + fullPath);
        try
        {
            System.IO.File.WriteAllText(fullPath, json);
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save settings data: " + e.Message);
        }
        return result;
    }

    public bool LoadJson()
    {
        bool result = false;
        string fullPath = GetFullPath();
        string json = System.IO.File.ReadAllText(fullPath);
        try
        {
            stageData = Stage.CreateScriptableObjectFromJSON(json);
            result = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to load settings data: " + e.Message);
        }
        return result;
    }



    [BoxGroup("�f�o�b�O"), Button("���݂̃{�^��", ButtonSizes.Medium), GUIColor(1, 0, 0, 1)]
    public void OnClickCurrentButton()
    {
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                TabletEditorUI tabletUi = child.gameObject.GetComponent<TabletEditorUI>();
                tabletUi.ApplyCurrentState();
                tabletUi.status = TabletPhase.Current;
            }
        }
        modeText.text = "��胂�[�h";
    }

    [BoxGroup("�f�o�b�O"), Button("�񓚂̃{�^��", ButtonSizes.Medium), GUIColor(1, 0, 0, 1)]
    public void OnClickAnswerButton()
    {
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                TabletEditorUI tabletUi = child.gameObject.GetComponent<TabletEditorUI>();
                tabletUi.ApplyAnswerState();
                tabletUi.status = TabletPhase.Answer;
            }
        }
        modeText.text = "�񓚃��[�h";
    }


    [BoxGroup("�f�o�b�O"), Button("������", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void InitStageData()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;

        if (stageData == null)
        {
            Debug.Log("�X�e�[�W�f�[�^���ݒ肳��Ă܂���B");
            return;
        }
        int size = stageData.Size;
        int x = stageData.X;
        int y = stageData.Y;
        if (size == 0 || x == 0 || y == 0)
        {
            Debug.LogError("�s���ȃf�[�^������܂��B");
            Debug.Log("Size:" + size);
            Debug.Log("X:" + x);
            Debug.Log("Y:" + y);
            return;
        }
        if (size % 2 != 0)
        {
            Debug.LogError("Size�͋������w�肵�Ă��������B");
            Debug.Log("Size:" + size);
        }

        float positionYg = 179f; //Y����
        float positionYk = 179f - size / 2; //Y�

        List<TabletRow> rows = new List<TabletRow>();
        TabletRow tabletRow = new TabletRow();
        List<TabletData> columns = new List<TabletData>();
        TabletData tabletData = new TabletData();
        tabletData.currentState = TabletState.None;
        tabletData.answerState = TabletState.None;
        tabletData.hintNum = 0;

        for (int j = 0; j < y; j++)
        {
            positionX = -400f;
            columns = new List<TabletData>();
            for (int i = 0; i < x; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj;
                if (i % 2 == 0)
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYg, 0.0f), Quaternion.identity);
                }
                else
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYk, 0.0f), Quaternion.identity);
                }
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.NoneEditor;
                tabletUi.answerState = TabletState.NoneEditor;
                tabletUi.row = j;
                tabletUi.column = i;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
                columns.Add(tabletData);
            }
            //positionY = positionY - size;
            positionYg = positionYg - size;
            positionYk = positionYk - size;
            tabletRow.Columns = columns;
            rows.Add(tabletRow);
        }
        stageData.SetRows(rows);
    }

    [BoxGroup("�f�o�b�O"), Button("��聨��", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void SaveQuestionToAnswer()
    {
        List<TabletEditorUI> tablets = new List<TabletEditorUI>();
        int index = 0;
        int tabletsMax = 0;
        foreach (Transform child in stage.gameObject.transform)
        {
            if (child.gameObject.name == "EditableTablet(Clone)")
            {
                tablets.Add(child.gameObject.GetComponent<TabletEditorUI>());
                index++;
            }
        }
        tabletsMax = index;
        for (int i = 0; i < tabletsMax - 1; i++)
        {
            for (int j = i + 1; j < tabletsMax; j++)
            {
                // row�����������ɕ��ׂ�
                if (tablets[i].row > tablets[j].row ||
                    (tablets[i].row == tablets[j].row && tablets[i].column > tablets[j].column))
                {
                    // tablets[i] �� tablets[j] ����������
                    var temp = tablets[i];
                    tablets[i] = tablets[j];
                    tablets[j] = temp;
                }
            }
        }
        int maxRow = 0;
        int maxColumn = 0;
        foreach (var tablet in tablets)
        {
            //Debug.Log("row: " + tablet.row);
            //Debug.Log("column: " + tablet.column);
            if (tablet.row > maxRow)
            {
                maxRow = tablet.row;//�ő�Row
            }
            if (tablet.column > maxColumn)
            {
                maxColumn = tablet.column;//�ő�Column
            }
        }

        Debug.Log("maxRow: " + maxRow);
        Debug.Log("maxColumn: " + maxColumn);
        Debug.Log("==========================TabletData==========================");
        List<TabletRow> rows = new List<TabletRow>();
        TabletRow tabletRow = new TabletRow();
        List<TabletData> columns = new List<TabletData>();
        TabletData tabletData = new TabletData();
        index = 0;
        while (true)
        {
            if (index >= tabletsMax)
            {
                Debug.Log(index);
                break;
            }
            tabletData = new TabletData();
            tabletData.currentState = tablets[index].currentState;
            tabletData.answerState = tablets[index].currentState;
            tabletData.hintNum = tablets[index].hintNum;
            columns.Add(tabletData);
            Debug.Log("Row: " + tablets[index].row);
            Debug.Log("Column: " + tablets[index].column);
            if (tablets[index].column == maxColumn)
            {
                tabletRow.Columns = columns;
                rows.Add(tabletRow);
                tabletRow = new TabletRow();
                columns = new List<TabletData>();
                index++;
                continue;
            }
            index++;
        }
        stageData.SetRows(rows);
        SaveRemain();
        if (SaveJson())
        {
            Debug.Log("�X�e�[�W�̕ۑ��ɐ������܂����B");
        }
        else
        {
            Debug.LogError("�X�e�[�W�̕ۑ��Ɏ��s���܂����B");
        }
        LoadStageData();
    }

        [BoxGroup("�f�o�b�O"), Button("�Z�[�u", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void SaveStageData()
    {
        List<TabletEditorUI> tablets = new List<TabletEditorUI>();
        int index = 0;
        int tabletsMax = 0;
        foreach (Transform child in stage.gameObject.transform)
        {
            if(child.gameObject.name == "EditableTablet(Clone)")
            {
                tablets.Add(child.gameObject.GetComponent<TabletEditorUI>());
                index++;
            }
        }
        tabletsMax = index;
        for (int i = 0; i < tabletsMax - 1; i++)
        {
            for (int j = i + 1; j < tabletsMax; j++)
            {
                // row�����������ɕ��ׂ�
                if (tablets[i].row > tablets[j].row ||
                    (tablets[i].row == tablets[j].row && tablets[i].column > tablets[j].column))
                {
                    // tablets[i] �� tablets[j] ����������
                    var temp = tablets[i];
                    tablets[i] = tablets[j];
                    tablets[j] = temp;
                }
            }
        }
        int maxRow = 0;  
        int maxColumn = 0;
        foreach (var tablet in tablets)
        {
            //Debug.Log("row: " + tablet.row);
            //Debug.Log("column: " + tablet.column);
            if (tablet.row > maxRow)
            {
                maxRow = tablet.row;//�ő�Row
            }
            if (tablet.column > maxColumn)
            {
                maxColumn = tablet.column;//�ő�Column
            }
        }

        Debug.Log("maxRow: " + maxRow);
        Debug.Log("maxColumn: " + maxColumn);
        Debug.Log("==========================TabletData==========================");
        List<TabletRow> rows = new List<TabletRow>();
        TabletRow tabletRow = new TabletRow();
        List<TabletData> columns = new List<TabletData>();
        TabletData tabletData = new TabletData();
        index = 0;
        while (true)
        {
            if (index >= tabletsMax)
            {
                Debug.Log(index);
                break;
            }
            tabletData = new TabletData();
            tabletData.currentState = tablets[index].currentState;
            tabletData.answerState = tablets[index].answerState;
            tabletData.hintNum = tablets[index].hintNum;
            columns.Add(tabletData);
            Debug.Log("Row: " + tablets[index].row);
            Debug.Log("Column: " + tablets[index].column);
            if (tablets[index].column == maxColumn)
            {
               tabletRow.Columns = columns;
               rows.Add(tabletRow);
               tabletRow = new TabletRow();
               columns = new List<TabletData>();
               index++;
               continue;
           }
           index++;
       }
       stageData.SetRows(rows);
       SaveRemain();
        if (SaveJson())
        {
            Debug.Log("�X�e�[�W�̕ۑ��ɐ������܂����B");
        }
        else
        {
            Debug.LogError("�X�e�[�W�̕ۑ��Ɏ��s���܂����B");
        }
    }

    [BoxGroup("�f�o�b�O"), Button("���[�h", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void LoadStageData()
    {
        if (LoadJson())
        {
            Debug.Log("�X�e�[�W�̓Ǎ��ɐ������܂����B");
        }
        else
        {
            Debug.LogError("�X�e�[�W�̓Ǎ��Ɏ��s���܂����B");
            return;
        }
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        if (stageData == null)
        {
            Debug.Log("�X�e�[�W�f�[�^���ݒ肳��Ă܂���B");
            return;
        }
        int size = stageData.Size;
        int x = stageData.X;
        int y = stageData.Y;
        if (size == 0 || x == 0 || y == 0)
        {
            Debug.Log("�s���ȃf�[�^������܂��B");
            Debug.Log("Size:" + size);
            Debug.Log("X:" + x);
            Debug.Log("Y:" + y);
            return;
        }
        if (size % 2 != 0)
        {
            Debug.LogError("Size�͋������w�肵�Ă��������B");
            Debug.Log("Size:" + size);
        }
        float positionYg = 179f; //Y����
        float positionYk = 179f - size / 2; //Y�

        List<TabletRow> rows = stageData.GetRows();
        int rowIndex = 0;
        int columnIndex = 0;
        foreach (TabletRow row in rows)
        {
            positionX = -400f;
            columnIndex = 0;
            foreach (TabletData data in row.Columns)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj;
                if (columnIndex % 2 == 0)
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYg, 0.0f), Quaternion.identity);
                }
                else
                {
                    tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionYk, 0.0f), Quaternion.identity);
                }
                //GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                Debug.Log("currentState:");
                Debug.Log(data.currentState);
                if(data.currentState == TabletState.None)
                {
                    data.currentState = TabletState.NoneEditor;
                }
                if (data.answerState == TabletState.None)
                {
                    data.answerState = TabletState.NoneEditor;
                }
                tabletUi.currentState = data.currentState;
                tabletUi.answerState = data.answerState;
                tabletUi.hintNum = data.hintNum;
                tabletUi.row = rowIndex;
                tabletUi.column = columnIndex;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
                columnIndex++;
            }
            //positionY = positionY - size;
            positionYg = positionYg - size;
            positionYk = positionYk - size;
            rowIndex++;
        }
    }

    [BoxGroup("�f�o�b�O"), Button("�e�X�g", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void Test()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        if (stageData == null)
        {
            Debug.Log("�X�e�[�W�f�[�^���ݒ肳��Ă܂���B");
            return;
        }
        int size = stageData.Size;
        int x = stageData.X;
        int y = stageData.Y;
        if (size == 0 || x == 0 || y == 0)
        {
            Debug.Log("�s���ȃf�[�^������܂��B");
            Debug.Log("Size:" + size);
            Debug.Log("X:" + x);
            Debug.Log("Y:" + y);
            return;
        }
        /*
        List<Field> fields = stageData.field;
        foreach (Field field in fields)
        {
            foreach(FieldX fieldX in field.fieldx)
            {

            }
        }
        */
        for (int j = 0; j < y; j++)
        {
            positionX = -400f;
            for (int i = 0; i < x; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.NoneEditor;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
            }
            positionY = positionY - size;
        }
    }

    [BoxGroup("�f�o�b�O"), Button("�e�X�gxx", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void Test01()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;

        if(stageData == null)
        {
            Debug.Log("�X�e�[�W�f�[�^���ݒ肳��Ă܂���B");
            return;
        }
        int size = stageData.Size;
        int x = stageData.X;  
        int y = stageData.Y;
        if(size == 0 || x == 0 || y == 0)
        {
            Debug.Log("�s���ȃf�[�^������܂��B");
            Debug.Log("Size:" + size);
            Debug.Log("X:" + x);
            Debug.Log("Y:" + y);
            return;
        }

        for (int j = 0; j < y; j++)
        {
            positionX = -400f;
            for (int i = 0; i < x; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                RectTransform rectTransform = tabletObj.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                Image image = tabletObj.GetComponentInChildren<Image>();
                rectTransform = image.gameObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(size, size);
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.NoneEditor;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                positionX = positionX + size;
            }
            positionY = positionY - size;
        }
    }

    [BoxGroup("�f�o�b�O"), Button("�X�e�[�W�\��", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void ShowStage()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        for (int j = 0; j < 10; j++)
        {
            positionX = -400f;
            for (int i = 0; i < 26; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load(editableTabletPath);
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                positionX = positionX + 32;
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.None;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                //Debug.Log(tabletUi);
            }
            positionY = positionY - 32;
        }
    }

    [BoxGroup("�f�o�b�O"), Button("�X�e�[�W�\��32", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void ShowStage32()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        for (int j = 0; j < 13; j++)
        {
            positionX = -400f;
            for (int i = 0; i < 26; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load("Prefabs/Tablet32");
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                positionX = positionX + 32;
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.None;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                //Debug.Log(tabletUi);
            }
            positionY = positionY - 32;
        }
    }

    [BoxGroup("�f�o�b�O"), Button("�X�e�[�W�\��50", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void ShowStage50()
    {
        AllDestroy();
        float positionX = -400f;
        float positionY = 179f;
        for (int j = 0; j < 9; j++)
        {
            positionX = -400f;
            for (int i = 0; i < 17; i++)
            {
                GameObject tabletPrefab = (GameObject)Resources.Load("Prefabs/Tablet50");
                GameObject tabletObj = Instantiate(tabletPrefab, new Vector3(positionX, positionY, 0.0f), Quaternion.identity);
                tabletObj.transform.SetParent(stage.transform, false);
                positionX = positionX + 50;
                TabletEditorUI tabletUi = tabletObj.GetComponent<TabletEditorUI>();
                tabletUi.currentState = TabletState.Hint;
                tabletUi.answerState = TabletState.None;
                tabletUi.Init();
                tabletUi.ApplyCurrentState();
                //Debug.Log(tabletUi);
            }
            positionY = positionY - 50;
        }
    }

    [BoxGroup("�f�o�b�O"), Button("�S�폜", ButtonSizes.Medium), GUIColor(1, 1, 0, 1)]
    public void AllDestroy()
    {
        foreach (Transform child in stage.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
