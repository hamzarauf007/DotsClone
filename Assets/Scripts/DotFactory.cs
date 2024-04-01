using UnityEngine;

public class DotFactory : MonoBehaviour, IDotFactory
{
    public Transform board;
    public DotPool dotPool;

    public int interval = 4;

    public Dot GetDot(int col, int row)
    {
        // Retrieve a dot from the pool and set its position
        Dot dot = dotPool.GetDot();
        dot.transform.position = new Vector2(col, row);
        dot.transform.parent = board;
        dot.gameObject.name = GenerateDotName(col, row);
        // Here, you could also set the dot's value and color
        int value = CalculateValueForDot(); // Implement this method based on your game logic
        Color çolor = CalculateColorForValue(value); // Implement this method based on your game logic
        // dot.Initialize(value, color);
        dot.Setup(value, row, col, çolor);
        return dot;
    }

    public void ReturnDot(Dot dot)
    {
        dotPool.ReturnDotToPool(dot);
    }

    private int CalculateValueForDot()
    {
        int exponent = Random.Range(1, interval);

        // Calculate 2 to the power of 'exponent'
        return (int)Mathf.Pow(2, exponent);
    }

    private Color CalculateColorForValue(int value)
    {
        // Placeholder for color determination logic
        // This method should map the value to a specific color
        if (dotPool.sequence.ContainsKey(value))
        {
            // Retrieve the value associated with the key
            return dotPool.sequence[value];
        }
        else
        {
            Debug.LogError($"Color against value {value} doesn't exist");
            return Color.black;
        }
    }
    
    public string GenerateDotName(int col, int row)
    {
        return $"Dot ({col},{row})";
    }
}
