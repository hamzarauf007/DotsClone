using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MyCustomType))]
public class MyCustomTypeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        // Custom GUI drawing code
        EditorGUI.EndProperty();
    }
}

[System.Serializable]
public class MyCustomType
{
    public int intValue;
    public float floatValue;
}