using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(CustomLabelAttribute))]
public class CustomLabelDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var newLabel = attribute as CustomLabelAttribute;
        EditorGUI.PropertyField(position, property, new GUIContent(newLabel.Value), true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }
}

[CustomPropertyDrawer(typeof(ColorMeAttribute))]
public class ColorMeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var colorMe = attribute as ColorMeAttribute;
        if (colorMe != null) GUI.color = colorMe.color;
        EditorGUI.PropertyField(position, property, label);
        GUI.color = Color.white;
    }
}
#endif