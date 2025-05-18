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
        TileEffectEnum[] grid = card.Grid;
        GUILayout.BeginVertical("box");
        for (int i = 0; i < 5; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 5; j++)
            {
                int index = i * 5 + j;
                switch (grid[index])
                {
                    case TileEffectEnum.None:
                        GUI.color = Color.gray;
                        break;
                    case TileEffectEnum.Center:
                        GUI.color = Color.white;
                        break;
                    case TileEffectEnum.Pawn:
                        GUI.color = Color.yellow;
                        break;
                    case TileEffectEnum.Effect:
                        GUI.color = Color.red;
                        break;
                    case TileEffectEnum.PawnAndEffect:
                        GUI.color = Color.magenta;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (i == 2 && j == 2)
                {
                    GUI.enabled = false;                    
                }
                if (GUILayout.Button(String.Empty, GUILayout.Width(30), GUILayout.Height(30)))
                {
                    if ((int)grid[index] >= Enum.GetValues(typeof(TileEffectEnum)).Length - 2)
                    {
                        grid[index] = TileEffectEnum.None;
                    }
                    else
                    {
                        grid[index]++;
                    }
                }
                
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        card.Grid = grid;
    }
}
