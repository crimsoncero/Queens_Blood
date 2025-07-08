using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public enum CardCostEnum
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
public class CardData : ScriptableObject
{
   
    
    [SerializeField] private UnityEvent<CardData> _onPlay;
    [SerializeField] private UnityEvent<CardData> _onRemoved;
    [SerializeField] private UnityEvent<CardData> _onChange;
    [SerializeField] private CardEffects.TriggerType _triggerType;

    [SerializeField] private string _name;
    [SerializeField, Min(0)] private int _id;
    [SerializeField] private CardCostEnum _cost;
    [SerializeField, Min(0)] private int _power;
    [SerializeField] private Sprite _sprite;
    
    [SerializeField] private CardRarityEnum _rarity;

    [SerializeField] private CardGrid _grid;
    
    public CardGrid Grid => _grid;
    public string Name => _name;
    public int ID => _id;
    public int Power => _power;
    public Sprite Sprite => _sprite;
    public CardRarityEnum Rarity => _rarity;
    public CardCostEnum Cost => _cost;
    public UnityEvent<CardData> OnPlay => _onPlay;
    public UnityEvent<CardData> OnRemoved => _onRemoved;
    public UnityEvent<CardData> OnChange => _onChange;

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


    public static bool IsValid(CardData card)
    {
        if (card == null) return false;

        // ReSharper disable once ReplaceWithSingleAssignment.True
        var isValid = true;
        
        if(card._name == string.Empty)
            isValid = false;
        if(card._power < 0)
            isValid = false;
        if(card._sprite == null)
            isValid = false;
        

        return isValid;

    }
}
