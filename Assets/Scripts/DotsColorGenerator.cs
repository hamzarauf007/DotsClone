using System.Collections.Generic;
using UnityEngine;

public class DotsColorGenerator : MonoBehaviour
{
    [SerializeField]
    private int maxSeq = 10000;
    [SerializeField]
    private int colorSpread = 1;
    
    public readonly Dictionary<int, Color> sequence = new Dictionary<int, Color>();
    
    private void Awake()
    {
        GenerateSequenceUpToMax(maxSeq);
    }
    
    private void GenerateSequenceUpToMax(int maxValue)
    {
        int currentValue = 2;

        for (int i = 0; i < maxValue;)
        {
            float frequency = colorSpread;
            float red = Mathf.Sin(frequency * i + 0) * 0.5f + 0.5f;
            float green = Mathf.Sin(frequency * i + 2) * 0.5f + 0.5f;
            float blue = Mathf.Sin(frequency * i + 4) * 0.5f + 0.5f;
            Color color = new Color(red, green, blue);
            
            // Debug.LogError(currentValue);
            sequence.Add(currentValue,color);
            currentValue *= 2;
            i = currentValue;
        }
    }
}
