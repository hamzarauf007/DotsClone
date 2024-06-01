using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;



[CustomEditor(typeof(Player), true)]
public class ReflectiveInspector : UnityEditor.Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Player monoBehaviour = (Player)target;
        Type type = monoBehaviour.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo field in fields)
        {
            object value = field.GetValue(monoBehaviour);
            EditorGUILayout.LabelField(field.Name, value != null ? value.ToString() : "null");
        }
    }
}