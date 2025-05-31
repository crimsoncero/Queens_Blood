using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Sprite))]
public class SpriteDrawer : PropertyDrawer
{
    private static GUIStyle STYLE = new GUIStyle();
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var ident = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect spriteRect;
        
        spriteRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        property.objectReferenceValue = EditorGUI.ObjectField(spriteRect, property.name, property.objectReferenceValue, typeof(Sprite), false);

        if (Event.current.type != EventType.Repaint || property.objectReferenceValue == null)
            return;
        
        Sprite sp = property.objectReferenceValue as Sprite;
        
        spriteRect.y += EditorGUIUtility.singleLineHeight + 4;
        spriteRect.width = 64;
        spriteRect.height = 64;
        STYLE.normal.background = sp.texture;
        STYLE.Draw(spriteRect, GUIContent.none, false, false , false, false);
        
        EditorGUI.indentLevel = ident;

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 70f;
    }
}
