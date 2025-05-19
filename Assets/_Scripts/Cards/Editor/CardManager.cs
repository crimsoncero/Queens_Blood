using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CardManager : EditorWindow
{
    private VisualElement _rightPane;
    private ListView _leftPane;
    
    
    
    [MenuItem("Tools/Card Manager")]
    public static void ShowExample()
    {
        CardManager wnd = GetWindow<CardManager>();
        wnd.titleContent = new GUIContent("Card Manager");
    }

    public void CreateGUI()
    {
        var cardElementTree =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/_Scripts/Cards/Editor/Card Manager/CardElement.uxml");
        var cardElement = Instantiate(cardElementTree);
        
        // Get a list of all sprites in the project
        var allObjectGuids = AssetDatabase.FindAssets("t:CardScriptable");
        var allCards = new List<CardScriptable>();
        foreach (var guid in allObjectGuids)
        {
            allCards.Add(AssetDatabase.LoadAssetAtPath<CardScriptable>(AssetDatabase.GUIDToAssetPath(guid)));
        }

        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        
        rootVisualElement.Add(splitView);
        _leftPane = new ListView();
        splitView.Add(_leftPane);
        _rightPane = new VisualElement();
        splitView.Add(_rightPane);

        _leftPane.makeItem = () => new Label();
        _leftPane.bindItem = (item, index) => { (item as Label).text = allCards[index].Name; };
        _leftPane.itemsSource = allCards;
        _leftPane.selectionChanged += OnCardSelectionChanged;

    }
    
    private void OnCardSelectionChanged(IEnumerable<object> selectedCards)
    {
        _rightPane.Clear();
        InspectorElement i = new InspectorElement(selectedCards.First() as CardScriptable);
        _rightPane.Add(i);
        _leftPane.RefreshItems();
    }
    

}
