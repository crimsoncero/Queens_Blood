using UnityEditor;
using UnityEngine;

public class FolderFinder
{
    [MenuItem("Tools/Find Scripts Folder In Project &s")]
    public static void ShowScriptsFolderInProject()
    {
        var folder = AssetDatabase.LoadAssetAtPath<Object>("Assets/_Scripts");
        if (folder != null)
        {
            EditorApplication.ExecuteMenuItem("Window/General/Project");
            
            Selection.activeObject = folder;
        }
        else
        {
            Debug.LogWarning("Folder not found!");
        }
    }
}