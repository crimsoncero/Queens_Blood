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

    public void DoNothing(CardScriptable card)
    {
        return;
    }

    public void IncreaseAlliesPower(CardScriptable card, int power)
    {
        
    }
    
}
