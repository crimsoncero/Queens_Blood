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

    public void DoNothing()
    {
        return;
    }

    public void IncreaseAlliesPower(int amount)
    {
        
    }
    
}
