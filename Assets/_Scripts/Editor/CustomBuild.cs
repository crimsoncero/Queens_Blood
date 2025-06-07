using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;

public class CustomBuild : MonoBehaviour
{
    [MenuItem("Tools/Custom Build/Build Windows #b")]
    public static void BuildWindows()
    {
        string path = $"Builds/WindowsBuild_{System.DateTime.Now:yyyyMMdd_HHmmss}";

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string[] scenes = new string[]
        {
            "Assets/Scenes/GET BAMBOOZLED.unity",
            "Assets/Scenes/Game.unity"
        };

        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = $"{path}/QueensBlood.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None
        };

        bool allCardsValid = true;
        foreach (var asset in AssetDatabase.FindAssets("t:CardScriptable"))
        {
            var cPath = AssetDatabase.GUIDToAssetPath(asset);
            var card = AssetDatabase.LoadAssetAtPath<CardScriptable>(cPath);

            if (!CardScriptable.IsValid(card))
                allCardsValid = false;
        }

        if (!allCardsValid)
        {
            if (!EditorUtility.DisplayDialog("Build Operation",
                    "Not all cards are valid, do you wish to continue the build?", "Yes",
                    "No")) return;
        }

        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        else if (summary.result == BuildResult.Failed)
            Debug.LogError("Build failed");
    }
}
