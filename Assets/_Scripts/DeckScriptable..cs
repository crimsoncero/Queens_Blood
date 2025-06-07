using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Cards/Deck")]
public class DeckScriptable : ScriptableObject
{
    public List<CardScriptable> cards = new List<CardScriptable>();
}
