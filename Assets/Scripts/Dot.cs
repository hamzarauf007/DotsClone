using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dot - represents a dot on the board
/// </summary>
public class Dot : MonoBehaviour
{

    public int DotType = 0;
    public GameObject highlight = null;
    public int Row = 0;
    public int Column = 0;
    public Color color;

    // Use this for initialization
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(int dotType, int row, int column)
    {
        DotType = dotType;
        Row = row;
        Column = column;
    }

    public void Activate()
    {
        this.highlight.SetActive(true);
    }

    public void Deactivate()
    {
        this.highlight.SetActive(false);
    }
}
