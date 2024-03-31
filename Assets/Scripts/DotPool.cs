using System.Collections.Generic;
using UnityEngine;

public class DotPool : MonoBehaviour
{
    public int initialPoolSize = 50;
    public int maxSeq = 10000;
    public int colorSpread = 1;
    
    public GameObject dotPrefab;
    
    private Queue<Dot> availableDots = new Queue<Dot>();
    private Dictionary<int, Color> sequence = new Dictionary<int, Color>();

    private void Start()
    {
        GenerateSequenceUpToMax(maxSeq);
        InitializePool();
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
            
            Debug.LogError(currentValue);
            sequence.Add(currentValue,color);
            currentValue *= 2;
            i = currentValue;
        }
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateDotInPool();
        }
    }

    private void CreateDotInPool()
    {
        GameObject newDotObj = Instantiate(dotPrefab, transform);
        newDotObj.SetActive(false);
        Dot newDot = newDotObj.GetComponent<Dot>();
        availableDots.Enqueue(newDot);
    }

    public Dot GetDot()
    {
        if (availableDots.Count == 0)
        {
            CreateDotInPool();
        }

        Dot dot = availableDots.Dequeue();
        dot.gameObject.SetActive(true);
        return dot;
    }

    public void ReturnDotToPool(Dot dot)
    {
        dot.gameObject.SetActive(false);
        availableDots.Enqueue(dot);
    }
    
    
}