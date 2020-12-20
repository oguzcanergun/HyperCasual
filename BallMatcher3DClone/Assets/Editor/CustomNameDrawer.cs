using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof(CustomNameAttribute))] public class CustomNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.ObjectField(position, property, new GUIContent(((CustomNameAttribute)attribute).names[pos]));
        }
        catch
        {
            EditorGUI.ObjectField(position, property, label);
            throw;
        }
    }
}
