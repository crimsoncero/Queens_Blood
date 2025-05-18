using System;
using System.Drawing;
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
        ShowGrid();

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

    private void ShowGrid()
    {
        GUILayout.Space(10);
        GUILayout.Label("Grid", EditorStyles.boldLabel);
        
        CardScriptable card = (CardScriptable)target;
        GUILayout.BeginVertical("box");
        for (int i = 0; i < CardScriptable.GRID_HEIGHT; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < CardScriptable.GRID_WIDTH; j++)
            {
                TileEffectEnum tile = card[i, j];
                var index = i * CardScriptable.GRID_HEIGHT + j;

                string texName = tile switch
                {
                    TileEffectEnum.None => "TileEmpty",
                    TileEffectEnum.Center => "TileCenter",
                    TileEffectEnum.Pawn => "TilePawn",
                    TileEffectEnum.Effect => "TileEffect",
                    TileEffectEnum.PawnAndEffect => "TilePawnEffect",
                    _ => throw new ArgumentOutOfRangeException()
                };

                Texture tileTex =
                    AssetDatabase.LoadAssetAtPath<Texture>(
                        "Assets/_Scripts/Cards/Editor/Tile Images/" + texName + ".png");

                GUIContent content = new GUIContent(String.Empty, tileTex);
                var buttonSkin = GUI.skin.button;
                GUI.skin.button.padding = new RectOffset(5, 5, 5, 5);
                
                if (i == CardScriptable.GRID_WIDTH / 2 && j == CardScriptable.GRID_HEIGHT / 2)
                {
                    GUI.enabled = false;                    
                }
                
                if (GUILayout.Button(content,GUILayout.Width(50), GUILayout.Height(50)))
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
                GUI.skin.button = buttonSkin;
                card[i, j] = tile;

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        
        
        if(GUILayout.Button("Reset Grid"))
           ResetGrid();
    }

    private void ResetGrid()
    {
        CardScriptable card = (CardScriptable)target;
        for (int i = 0; i < CardScriptable.GRID_HEIGHT; i++)
        {
            for (int j = 0; j < CardScriptable.GRID_WIDTH; j++)
            {
                if(i == CardScriptable.GRID_HEIGHT / 2 && j == CardScriptable.GRID_WIDTH / 2)
                    card[i, j] = TileEffectEnum.Center;
                else
                    card[i, j] = TileEffectEnum.None;
            }
        }
    }
}
