using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

[Overlay(typeof(SceneView), "Scene Selector", true)]
public class SceneSelectorToolbar : Overlay
{
    private PopupField<string> _sceneDropdown;
    private string[] _scenePaths;
    private string[] _sceneNames;

    public override VisualElement CreatePanelContent()
    {
        var root = new VisualElement();
        root.style.flexDirection = FlexDirection.Row;

        _scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" })
            .Select(AssetDatabase.GUIDToAssetPath)
            .ToArray();

        _sceneNames = _scenePaths
            .Select(path => Path.GetFileNameWithoutExtension(path))
            .ToArray();

        if (_sceneNames.Length == 0)
        {
            root.Add(new Label("No scenes found in Assets/Scenes"));
            return root;
        }

        _sceneDropdown = new PopupField<string>("Open Scene", _sceneNames.ToList(), 0);
        _sceneDropdown.RegisterValueChangedCallback(evt =>
        {
            var path = _scenePaths[_sceneDropdown.index];
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(path);
            }
        });

        root.Add(_sceneDropdown);
        return root;
    }
}
