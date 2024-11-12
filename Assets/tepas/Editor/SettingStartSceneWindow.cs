//  SettingStartSceneWindow.cs
//  http://kan-kikuchi.hatenablog.com/entry/playModeStartScene
//  Created by kan.kikuchi on 2017.09.30.

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SettingStartSceneWindow : EditorWindow
{

    //設定したシーンのパスを保存するKEY
    private const string SAVE_KEY = "StartScenePathKey";

    //メニューからウィンドウを表示
    [MenuItem("Window/SettingStartSceneWindow")]
    public static void Open()
    {
        SettingStartSceneWindow.GetWindow<SettingStartSceneWindow>(typeof(SettingStartSceneWindow));
    }

    private void OnEnable()
    {
        string startScenePath = EditorUserSettings.GetConfigValue(SAVE_KEY);
        if (!string.IsNullOrEmpty(startScenePath))
        {
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(startScenePath);
            if (sceneAsset == null)
            {
                Debug.LogWarning(startScenePath + "がありません");
            }
            else
            {
                EditorSceneManager.playModeStartScene = sceneAsset;
            }
        }
    }

    private void OnGUI()
    {
        string beforeScenePath = "";
        if (EditorSceneManager.playModeStartScene != null)
        {
            beforeScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
        }
        EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Start Scene"), EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);

        string afterScenePath = "";
        if (EditorSceneManager.playModeStartScene != null)
        {
            afterScenePath = AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene);
        }
        if (beforeScenePath != afterScenePath)
        {
            EditorUserSettings.SetConfigValue(SAVE_KEY, afterScenePath);
        }
    }

}