using UnityEngine;
using TMPro;

public class Dot : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private TextMeshPro text;
    
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
        // this.highlight.SetActive(true);
    }

    public void Deactivate()
    {
        // this.highlight.SetActive(false);
    }
}
