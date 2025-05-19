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
    private VisualElement _managerElement;
    
    private List<CardScriptable> _cardList = new List<CardScriptable>();
    
    
    [MenuItem("Tools/Card Manager")]
    public static void ShowExample()
    {
        CardManager wnd = GetWindow<CardManager>();
        wnd.titleContent = new GUIContent("Card Manager");
    }

    public void CreateGUI()
    {
        var managerElementTree = 
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/_Scripts/Cards/Editor/Card Manager/CardManager.uxml");
         _managerElement = managerElementTree.Instantiate();
        
         FindAllCards();
        
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        
        rootVisualElement.Add(splitView);
        splitView.Add(_managerElement);
        _rightPane = new VisualElement();
        splitView.Add(_rightPane);
        
        MultiColumnListView listView = _managerElement.Q<MultiColumnListView>();
        listView.itemsSource = _cardList;
        listView.selectionChanged += OnCardSelectionChanged;
        
    }
    
    private void OnCardSelectionChanged(IEnumerable<object> selectedCards)
    {
        _rightPane.Clear();
        InspectorElement i = new InspectorElement(selectedCards.First() as CardScriptable);
        _rightPane.Add(i);
    }

    private void FindAllCards()
    {
        var allObjectGuids = AssetDatabase.FindAssets("t:CardScriptable");
        _cardList = new List<CardScriptable>();
        foreach (var guid in allObjectGuids)
        {
            _cardList.Add(AssetDatabase.LoadAssetAtPath<CardScriptable>(AssetDatabase.GUIDToAssetPath(guid)));
        }  
    }

}
