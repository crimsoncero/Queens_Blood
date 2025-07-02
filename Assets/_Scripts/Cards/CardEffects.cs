using UnityEngine;

[CreateAssetMenu(fileName = "Card Effects", menuName = "Scriptable Objects/Card Effects")]
public class CardEffects : ScriptableObject
{
    public enum TriggerType
    {
        None,
        OnPlay,
        OnRemoved,
        OnChange,
    }

    public void DoNothing(CardData card)
    {
        return;
    }

    public void IncreaseAlliesPower(CardData card, int power)
    {
        
    }
    
}
