using System;
using System.Reflection;
using UnityEngine;
public class OtherPlayer : MonoBehaviour
{

    public Player player;
    
    
    // Start is called before the first frame update
    void Start()
    {


        Type playerType = player.GetType();

        // Get all fields of the Player class
        FieldInfo[] fields = playerType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            Debug.LogError("Field: " + field.Name);
        }

        // Get all methods of the Player class
        MethodInfo[] methods = playerType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (MethodInfo method in methods)
        {
            Debug.LogError("Method: " + method.Name);
        }
        
        MethodInfo healMethod = playerType.GetMethod("Heal");

        
        
        if (healMethod != null)
        {
            Debug.LogError("Its not null");
        }

        // Invoke the Heal method dynamically
        healMethod.Invoke(player, new object[]{10});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
