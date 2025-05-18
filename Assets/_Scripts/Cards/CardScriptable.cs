using System;
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
    public const int GRID_WIDTH = 5;
    public const int GRID_HEIGHT = 5;
    
    [SerializeField] private UnityEvent<int> _onPlay;
    [SerializeField] private UnityEvent<int> _onRemoved;
    [SerializeField] private UnityEvent<int> _onChange;
    [SerializeField] private CardEffects.TriggerType _triggerType;

    public TileEffectEnum[] Grid = new TileEffectEnum[GRID_WIDTH * GRID_HEIGHT]
    {
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.Center, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None
    };
    public TileEffectEnum this[int i, int j]
    {
        get => Grid[i * GRID_HEIGHT + j];
        set => Grid[i * GRID_HEIGHT + j] = value;
    }
    
}
