using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DeckScriptable))]
public class DeckEditor : Editor
{
    private SerializedProperty _cards;

    private void OnEnable()
    {
        _cards = serializedObject.FindProperty("cards");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Deck Cards", EditorStyles.boldLabel);

        for (int i = 0; i < _cards.arraySize; i++)
        {
            SerializedProperty card = _cards.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(card, GUIContent.none);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                _cards.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Empty Slot"))
        {
            _cards.InsertArrayElementAtIndex(_cards.arraySize);
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Validate Deck"))
        {
            ValidateDeck();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ValidateDeck()
    {
        DeckScriptable deck = (DeckScriptable)target;

        HashSet<CardScriptable> uniqueCards = new HashSet<CardScriptable>(deck.cards);
        int cardCount = deck.cards.Count;

        if (cardCount == 0)
        {
            Debug.LogWarning("Deck is empty");
        }
        else
        {
            Debug.Log($"Deck has {cardCount} cards");
        }

        if (cardCount != uniqueCards.Count)
        {
            Debug.LogWarning("Deck contains duplicate cards");
        }

        if (cardCount < 10)
        {
            Debug.LogWarning("Deck has fewer than 10 cards");
        }
        else
        {
            Debug.Log("Deck is valid. YAY!!!");
        }
    }
}
