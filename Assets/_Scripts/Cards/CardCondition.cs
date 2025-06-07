using UnityEngine;

public enum CardConditionType
{
    None,
    OpponentPowerGreaterThan,
    IsFirstCardPlayed,
    HasCardTag,
    RankEquals,
    TurnNumberEven
}

[System.Serializable]
public struct CardCondition
{
    public CardConditionType conditionType;

    public int intValue;
    public string stringValue;
    public CardRank rankValue;
}

public enum CardRank { Bronze, Silver, Gold, Legendary }
