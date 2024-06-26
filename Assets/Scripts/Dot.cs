﻿using UnityEngine;
using TMPro;

public class Dot : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private TextMeshPro text;
    [SerializeField] 
    private Animator anim;

    private static readonly int ScaleUp = Animator.StringToHash("ScaleUp");
    private static readonly int Up = Animator.StringToHash("PopUp");
    private static readonly int Falling = Animator.StringToHash("Falling");

    public int DotValue { get; private set; }
    public int Row { get; private set; }
    public int Column { get; private set; }
    
    public Color SpriteColor { get; private set; }
    
    public void InitialSetup(int dotValue, int row, int column, Color color)
    {
        DotValue = dotValue;
        Row = row;
        Column = column;
        SpriteColor = color;
        sprite.color = color;
        text.text = dotValue.ToString();
    }

    public void SetDotValueColor(int value, Color color)
    {
        DotValue = value;
        sprite.color = color;
        SpriteColor = color;
        text.text = value.ToString();
    }

    public void UpdateRow(int row)
    {
        Row = row;
    }

    public void Activate()
    {
        anim.SetBool(ScaleUp,true);
    }

    public void Deactivate()
    {
        anim.SetBool(ScaleUp,false);
    }

    public void PopUp()
    {
        anim.SetTrigger(Up);
    }

    public void FallingAnimation()
    {
        anim.SetTrigger(Falling);
    }
}
