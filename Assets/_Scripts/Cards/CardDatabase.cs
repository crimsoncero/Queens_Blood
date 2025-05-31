using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Scriptable Objects/CardDatabase")]
public class CardDatabase : ScriptableObject
{
    public List<CardScriptable> CardList = new List<CardScriptable>();
    
    public void FindAllCards()
    {
        var allObjectGuids = AssetDatabase.FindAssets("t:CardScriptable");
        CardList = new List<CardScriptable>();
        foreach (var guid in allObjectGuids)
        {
            CardList.Add(AssetDatabase.LoadAssetAtPath<CardScriptable>(AssetDatabase.GUIDToAssetPath(guid)));
        }    
    }
}
