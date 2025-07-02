using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CardManager : EditorWindow
{
    private const string CARD_PATH = "Assets/Scriptables/Cards/";
    
    private VisualElement _rightPane;
    private VisualElement _managerElement;
    private MultiColumnListView _listView;
    private List<CardData> _cardList = new List<CardData>();
    
    
    [MenuItem("Tools/Card Editor")]
    public static void ShowExample()
    {
        CardManager wnd = GetWindow<CardManager>();
        wnd.titleContent = new GUIContent("Card Editor");
        
    }

    private void OnEnable()
    {
        FindAllCards();
    }

    public void CreateGUI()
    {
        
        var managerElementTree = 
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/_Scripts/Cards/Editor/Card Manager/CardManager.uxml");
        _managerElement = managerElementTree.Instantiate();
         
        var newCardButton = _managerElement.Q<Button>("New_Card_Button");
        var renameCardsButton = _managerElement.Q<Button>("Rename_Cards_Button");

        newCardButton.clickable.clicked += CreateNewCard;
        renameCardsButton.clickable.clicked += RenameCards;
        
         
        var splitView = new TwoPaneSplitView(1, 300, TwoPaneSplitViewOrientation.Horizontal);
        
        rootVisualElement.Add(splitView);
        splitView.Add(_managerElement);
        _rightPane = new VisualElement();
        splitView.Add(_rightPane);
        _listView = _managerElement.Q<MultiColumnListView>();
        _listView.itemsSource = _cardList;
        _listView.selectionChanged += OnCardSelectionChanged;
        
    }
    
    private void OnCardSelectionChanged(IEnumerable<object> selectedCards)
    {
        _rightPane.Clear();
        InspectorElement i = new InspectorElement(selectedCards.First() as CardData);
        _rightPane.Add(i);
    }

    private void FindAllCards()
    {
        var allObjectGuids = AssetDatabase.FindAssets("t:CardData");
        _cardList = new List<CardData>();
        foreach (var guid in allObjectGuids)
        {
            _cardList.Add(AssetDatabase.LoadAssetAtPath<CardData>(AssetDatabase.GUIDToAssetPath(guid)));
        }  
    }

    [MenuItem("Tools/Rename Cards #r")]
    private static void RenameCards()
    {
        if (!EditorUtility.DisplayDialog("Rename Cards",
                "Do you want to rename all cards scriptable objects assets to match their name?", "Yes",
                "What?! NO!!")) return;
        
        
        foreach (var asset in AssetDatabase.FindAssets("t:CardData"))
        {
            var path = AssetDatabase.GUIDToAssetPath(asset);
            var card = AssetDatabase.LoadAssetAtPath<CardData>(path);
            
            if(!string.IsNullOrEmpty(card.Name))
                AssetDatabase.RenameAsset(path, $"{card.Name}");
        }


    }

    private void CreateNewCard()
    {
        var card = CreateInstance<CardData>();
        AssetDatabase.CreateAsset(card, CARD_PATH + "NewCard.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        FindAllCards();
        _listView.itemsSource = _cardList;
    }
    
    
}
