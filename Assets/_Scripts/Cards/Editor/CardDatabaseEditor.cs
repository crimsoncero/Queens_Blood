using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardDatabase))]
public class CardDatabaseEditor : Editor
{
    private CardDatabase _cardDatabase;
    
    private void OnEnable()
    {
        _cardDatabase = target as CardDatabase;
        _cardDatabase?.FindAllCards();
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Find All Cards"))
        {
            _cardDatabase.FindAllCards();
        }
        base.OnInspectorGUI();
    }
}
