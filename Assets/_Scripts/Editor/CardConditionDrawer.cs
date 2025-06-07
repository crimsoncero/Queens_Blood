using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CardCondition))]
public class CardConditionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var conditionProp = property.FindPropertyRelative("conditionType");
        CardConditionType type = (CardConditionType)conditionProp.enumValueIndex;

        int extraLines = type switch
        {
            CardConditionType.None => 0,
            CardConditionType.IsFirstCardPlayed => 0,
            CardConditionType.TurnNumberEven => 0,
            CardConditionType.OpponentPowerGreaterThan => 1,
            CardConditionType.HasCardTag => 1,
            CardConditionType.RankEquals => 1,
            _ => 0
        };

        return (extraLines + 1) * EditorGUIUtility.singleLineHeight + 4;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var conditionProp = property.FindPropertyRelative("conditionType");
        var intProp = property.FindPropertyRelative("intValue");
        var strProp = property.FindPropertyRelative("stringValue");
        var rankProp = property.FindPropertyRelative("rankValue");

        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(position, conditionProp);

        position.y += EditorGUIUtility.singleLineHeight + 2;

        switch ((CardConditionType)conditionProp.enumValueIndex)
        {
            case CardConditionType.OpponentPowerGreaterThan:
                EditorGUI.PropertyField(position, intProp, new GUIContent("Power Threshold"));
                break;
            case CardConditionType.HasCardTag:
                EditorGUI.PropertyField(position, strProp, new GUIContent("Tag"));
                break;
            case CardConditionType.RankEquals:
                EditorGUI.PropertyField(position, rankProp, new GUIContent("Rank"));
                break;
        }
    }
}
