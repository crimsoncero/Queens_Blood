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
    
    [SerializeField] private UnityEvent<CardScriptable> _onPlay;
    [SerializeField] private UnityEvent<CardScriptable> _onRemoved;
    [SerializeField] private UnityEvent<CardScriptable> _onChange;
    [SerializeField] private CardEffects.TriggerType _triggerType;

    [SerializeField] private string _name;
    [SerializeField] private int _power;
    [SerializeField] private int _rank;
    [SerializeField] private Sprite _sprite;
    
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
    
    public string Name => _name;
    public int Power => _power;
    public int Rank => _rank;
    public Sprite Sprite => _sprite;
    public UnityEvent<CardScriptable> OnPlay => _onPlay;
    public UnityEvent<CardScriptable> OnRemoved => _onRemoved;
    public UnityEvent<CardScriptable> OnChange => _onChange;

    private void OnValidate()
    {
        switch (_triggerType)
        {
            case CardEffects.TriggerType.None:
                _onPlay = null;
                _onRemoved = null;
                _onChange = null;
                break;
            case CardEffects.TriggerType.OnPlay:
                _onRemoved = null;
                _onChange = null;
                break;
            case CardEffects.TriggerType.OnRemoved:
                _onPlay = null;
                _onChange = null;
                break;
            case CardEffects.TriggerType.OnChange:
                _onPlay = null;
                _onRemoved = null;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public static bool IsValid(CardScriptable card)
    {
        if (card == null) return false;

        // ReSharper disable once ReplaceWithSingleAssignment.True
        var isValid = true;
        
        if(card._name == string.Empty)
            isValid = false;
        if(card._power < 0)
            isValid = false;
        if(card._rank < 0)
            isValid = false;
        if(card._sprite == null)
            isValid = false;
        if(card[2,2] != TileEffectEnum.Center)
            isValid = false;

        return isValid;

    }
}
