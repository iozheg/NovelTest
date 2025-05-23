using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

// [InitializeOnLoad]
// public static class DefaultSceneLoader
// {
//     static DefaultSceneLoader(){
//         EditorApplication.playModeStateChanged += LoadDefaultScene;
//     }

//     static void LoadDefaultScene(PlayModeStateChange state){
//         if (state == PlayModeStateChange.ExitingEditMode) {
//             EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
//         }

//         if (state == PlayModeStateChange.EnteredPlayMode) {
//             Debug.Log("LoadDefaultScene");
//             EditorSceneManager.LoadScene (0);
//         }
//     }
// }
[InitializeOnLoad]
public class EditorInit
{
    static EditorInit()
    {
        var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
        // var pathOfFirstScene = "Assets/HexKingdom/Scenes/TestScene.unity";
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
        EditorSceneManager.playModeStartScene = sceneAsset;
        Debug.Log(pathOfFirstScene + " was set as default play mode scene");
    }
}
#endif