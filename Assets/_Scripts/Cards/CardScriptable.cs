using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public enum TileEffectEnum
{
    None,
    Pawn,
    Effect,
    PawnAndEffect,
    Center,
}

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/Card")]
public class CardScriptable : ScriptableObject
{
    [SerializeField] private UnityEvent<int> _onPlay;
    [SerializeField] private UnityEvent<int> _onRemoved;
    [SerializeField] private UnityEvent<int> _onChange;
    [SerializeField] private CardEffects.TriggerType _triggerType;

    public TileEffectEnum[] Grid = new TileEffectEnum[25]
    {
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.Center, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None
    };





}
