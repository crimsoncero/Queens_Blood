using System;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

[CustomEditor(typeof(CardScriptable))]
public class CardEditor : Editor
{
    private SerializedProperty _triggerType;
    private SerializedProperty _onPlay;
    private SerializedProperty _onRemoved;
    private SerializedProperty _onChange;
    
    private void OnEnable()
    {
        _triggerType = serializedObject.FindProperty("_triggerType");
        _onPlay = serializedObject.FindProperty("_onPlay");
        _onRemoved = serializedObject.FindProperty("_onRemoved");
        _onChange = serializedObject.FindProperty("_onChange");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_triggerType);
        ShowEvent();
        ShowGridButtons();

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty((CardScriptable)target);
    }
    private void ShowEvent()
    {
        switch(_triggerType.GetEnumValue<CardEffects.TriggerType>())
        {
            case CardEffects.TriggerType.None:
                break;
            case CardEffects.TriggerType.OnPlay:
                EditorGUILayout.PropertyField(_onPlay);
                break;
            case CardEffects.TriggerType.OnRemoved:
                EditorGUILayout.PropertyField(_onRemoved);
                break;
            case CardEffects.TriggerType.OnChange:
                EditorGUILayout.PropertyField(_onChange);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ShowGridButtons()
    {
        CardScriptable card = (CardScriptable)target;
        GUILayout.BeginVertical("box");
        for (int i = 0; i < CardScriptable.GRID_HEIGHT; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < CardScriptable.GRID_WIDTH; j++)
            {
                TileEffectEnum tile = card[i, j];
                var index = i * CardScriptable.GRID_HEIGHT + j;
                GUI.color = tile switch
                {
                    TileEffectEnum.None => Color.gray,
                    TileEffectEnum.Center => Color.white,
                    TileEffectEnum.Pawn => Color.yellow,
                    TileEffectEnum.Effect => Color.red,
                    TileEffectEnum.PawnAndEffect => Color.magenta,
                    _ => throw new ArgumentOutOfRangeException()
                };

                if (i == CardScriptable.GRID_WIDTH / 2 && j == CardScriptable.GRID_HEIGHT / 2)
                {
                    GUI.enabled = false;                    
                }
                if (GUILayout.Button(String.Empty, GUILayout.Width(30), GUILayout.Height(30)))
                {
                    if ((int)tile >= Enum.GetValues(typeof(TileEffectEnum)).Length - 2)
                    {
                        tile = TileEffectEnum.None;
                    }
                    else
                    {
                        tile++;
                    }
                }
                GUI.enabled = true;

                card[i, j] = tile;

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }
}
