using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneSwitcher
{
    [MenuItem("Tools/Switch to Game %g")]
    public static void SwitchToMyScene()
    {
        // Prompt to save unsaved changes
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            // Replace with your scene path relative to Assets folder
            string scenePath = "Assets/Scenes/Game.unity";
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}