using System;
using UnityEngine;
using UnityEngine.Events;


public enum CardPawnEnum
{
    One,
    Two,
    Three,
    Replace,
}
public enum CardRarityEnum
{
    Standard,
    Legendary,
}

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/Card")]
public class CardScriptable : ScriptableObject
{
   
    
    [SerializeField] private UnityEvent<CardScriptable> _onPlay;
    [SerializeField] private UnityEvent<CardScriptable> _onRemoved;
    [SerializeField] private UnityEvent<CardScriptable> _onChange;
    [SerializeField] private CardEffects.TriggerType _triggerType;

    [SerializeField] private string _name;
    [SerializeField] private int _power;
    [SerializeField] private int _rank;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private CardGrid _grid;
    
    public CardGrid Grid => _grid;
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
        

        return isValid;

    }
}
