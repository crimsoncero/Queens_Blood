using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;

public class CustomBuild : MonoBehaviour
{
    [MenuItem("Tools/Custom Build/Build Windows %#b")]
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

        BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        else if (summary.result == BuildResult.Failed)
            Debug.LogError("Build failed");
    }
}
