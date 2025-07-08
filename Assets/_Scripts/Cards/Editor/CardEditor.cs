using System;
using System.Drawing;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

[CustomEditor(typeof(CardData))]
public class CardEditor : Editor
{
    private const string CARD_EFFECT_PATH = "Assets/Scriptables/Settings/Card Effects.asset";
    private CardEffects _cardEffects;
    
    private SerializedProperty _triggerType;
    private SerializedProperty _onPlay;
    private SerializedProperty _onRemoved;
    private SerializedProperty _onChange;
    private SerializedProperty _name;
    private SerializedProperty _power;
    private SerializedProperty _sprite;
    private SerializedProperty _rarity;
    private SerializedProperty _cost;
    private SerializedProperty _id;
    private CardData _card;
    private bool _isValid;
    
    private void Awake()
    {
        _cardEffects = AssetDatabase.LoadAssetAtPath<CardEffects>(CARD_EFFECT_PATH);
    }

    private void OnEnable()
    {
        _triggerType = serializedObject.FindProperty("_triggerType");
        _onPlay = serializedObject.FindProperty("_onPlay");
        _onRemoved = serializedObject.FindProperty("_onRemoved");
        _onChange = serializedObject.FindProperty("_onChange");
        _name = serializedObject.FindProperty("_name");
        _power = serializedObject.FindProperty("_power");
        _sprite = serializedObject.FindProperty("_sprite");
        _rarity = serializedObject.FindProperty("_rarity");
        _cost = serializedObject.FindProperty("_cost");
        _id = serializedObject.FindProperty("_id");
        _card = target as CardData;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        ShowBasicData();
        EditorGUILayout.PropertyField(_sprite);
        ShowGrid();
        ShowEvent();
        
        serializedObject.ApplyModifiedProperties();

        
        _isValid = CardData.IsValid(_card);
        if (!_isValid)
        {
            EditorGUILayout.HelpBox("Card values are not valid.", MessageType.Error);
        }
        

        EditorUtility.SetDirty((CardData)target);
    }

    private void ShowBasicData()
    {
        EditorGUILayout.PropertyField(_name);
        EditorGUILayout.PropertyField(_id);
        EditorGUILayout.PropertyField(_rarity);
        EditorGUILayout.PropertyField(_cost);
        EditorGUILayout.PropertyField(_power);
    }
    
    private void ShowEvent()
    {
        EditorGUILayout.PropertyField(_triggerType);
                
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

    private void OnValidate()
    {
    }

    private void ShowGrid()
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Grid", EditorStyles.boldLabel);
        if(GUILayout.Button("Reset Grid"))
            ResetGrid();
        GUILayout.EndHorizontal();

        CardData card = _card;
        GUILayout.BeginVertical("box");
        for (int i = 0; i < CardGrid.HEIGHT; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < CardGrid.WIDTH; j++)
            {
                TileEffectEnum tile = card.Grid[i, j];
                var index = i * CardGrid.HEIGHT + j;

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
                
                if (i == CardGrid.WIDTH / 2 && j == CardGrid.HEIGHT / 2)
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
                card.Grid[i, j] = tile;

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        
        
        
    }

    private void ResetGrid()
    {
        CardData card = (CardData)target;
        for (int i = 0; i < CardGrid.HEIGHT; i++)
        {
            for (int j = 0; j < CardGrid.WIDTH; j++)
            {
                if(i == CardGrid.HEIGHT / 2 && j == CardGrid.WIDTH / 2)
                    card.Grid[i, j] = TileEffectEnum.Center;
                else
                    card.Grid[i, j] = TileEffectEnum.None;
            }
        }
    }

}
