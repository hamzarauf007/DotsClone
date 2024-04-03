using System.Collections.Generic;
using UnityEngine;

public class DotPool : MonoBehaviour
{
    [SerializeField]
    private int initialPoolSize = 50;
    
    [SerializeField]
    private GameObject dotPrefab;
    
    private Queue<Dot> availableDots = new Queue<Dot>();

    private void Awake()
    {
        InitializePool();
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
        dot.transform.parent = transform;
        dot.gameObject.SetActive(false);
        availableDots.Enqueue(dot);
    }
}