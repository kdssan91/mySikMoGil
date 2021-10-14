using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
using UnityEditor.SceneManagement;

public class MenuSceneChanger : MonoBehaviour
{
    [MenuItem("SceneMenu/Main")]
    static void EditorMenu_LoadInMainScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity"); 
    }
    [MenuItem("SceneMenu/Intro")]
    static void EditorMenu_LoadInIntroScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Intro.unity");

    }
}
