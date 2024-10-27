#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class TemplateDir : EditorWindow
{
    string rootDir = "ProjectName";

    [MenuItem("Project/TemplateDir")]
    static void Init()
    {
        TemplateDir window = (TemplateDir)GetWindow(typeof(TemplateDir));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("�f�B���N�g���e���v���[�g�̎Q��", EditorStyles.boldLabel);
        GUILayout.Label("���`�̃f�B���N�g����ǂݍ��݁A�Q�[��������J�n���܂��B\n" +
            "--------------------------------\n" +
            "Editor/\n" +
            "Resources/\n" +
            "- Sound/\n" +
            "- Visual/Font/\n" +
            "- Visual/Background/\n" +
            "- Visual/Form/\n" +
            "- Visual/Character/\n" +
            "- Visual/Misc/\n" +
            "- Data/\n" +
            "Scenes/\n" +
            "--------------------------------", EditorStyles.label);
        rootDir = EditorGUILayout.TextField("�Q�[����", rootDir);
        if (GUILayout.Button("�ǂݍ���"))
        {
            Create(rootDir);
        }
    }

    void Create(string rootDir)
    {
        string basePath = Path.Combine("Assets", rootDir);
        string[] dirs = new string[]
        {
            "Editor",
            "Resources/Sound",
            "Resources/Visual/Font",
            "Resources/Visual/Background",
            "Resources/Visual/Form",
            "Resources/Visual/Character",
            "Resources/Visual/Misc",
            "Resources/Data",
            "Scenes/",
        };
        foreach (string dir in dirs)
        {
            string path = Path.Combine(basePath, dir);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        AssetDatabase.Refresh();
    }
}
#endif
