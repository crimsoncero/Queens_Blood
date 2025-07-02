using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Scriptable Objects/CardDatabase")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> CardList = new List<CardData>();
    
    public void FindAllCards()
    {
        var allObjectGuids = AssetDatabase.FindAssets("t:CardScriptable");
        CardList = new List<CardData>();
        foreach (var guid in allObjectGuids)
        {
            CardList.Add(AssetDatabase.LoadAssetAtPath<CardData>(AssetDatabase.GUIDToAssetPath(guid)));
        }    
    }
}
