using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CardListWindow : EditorWindow
{
    private const string CARDS_PATH = "Cards";
    private List<CardScriptable> _cardList = new List<CardScriptable>();
    
    
    
    [MenuItem("Tools/Cards")]
    public static void ShowWindow()
    {
        var wnd = GetWindow<CardListWindow>();
        wnd.titleContent = new GUIContent("Cards Viewer");
        
    }

    private void OnEnable()
    {
        _cardList = Resources.LoadAll<CardScriptable>(CARDS_PATH).ToList();
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        foreach (var card in _cardList)
        {
            Label label = new Label(card.Name);
            root.Add(label);
        }

    }
}
