using UnityEngine;

public class Dot : MonoBehaviour
{

    public int DotType = 0;
    public int Row = 0;
    public int Column = 0;
    public Color color;
    
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    public void Setup(int dotType, int row, int column)
    {
        DotType = dotType;
        Row = row;
        Column = column;
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
