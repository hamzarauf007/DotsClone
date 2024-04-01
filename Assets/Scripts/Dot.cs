using UnityEngine;
using TMPro;

public class Dot : MonoBehaviour
{

    public SpriteRenderer sprite;
    
    public int DotType = 0;
    public int Row = 0;
    public int Column = 0;
    
    [HideInInspector]
    public Color spriteColor;
    
    [SerializeField]
    private TextMeshPro text;
    
    
    public void Setup(int dotType, int row, int column, Color color)
    {
        DotType = dotType;
        Row = row;
        Column = column;
        spriteColor = color;
        sprite.color = color;
        text.text = dotType.ToString();
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
