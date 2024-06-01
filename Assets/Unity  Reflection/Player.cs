using System;
using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    private float speed;

    private void Start()
    {
        // Type playerType = typeof(Player);
        //
        // // Get all fields of the Player class
        // FieldInfo[] fields = playerType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        // foreach (FieldInfo field in fields)
        // {
        //     Debug.LogError("Field: " + field.Name);
        // }
        //
        // // Get all methods of the Player class
        // MethodInfo[] methods = playerType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        // foreach (MethodInfo method in methods)
        // {
        //     Debug.LogError("Method: " + method.Name);
        // }
    }

    private void Heal(int value)
    {
           Debug.Log("I love you mate");
    }
    
}