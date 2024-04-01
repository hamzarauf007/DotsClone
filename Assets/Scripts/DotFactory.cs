using UnityEngine;

public class DotFactory : MonoBehaviour, IDotFactory
{
    public Transform board;
    
    public DotPool dotPool;
    

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
    
    // ma abi isko name assign karna laga tha aur ya soch raha tha ka yahe pa he isko ma transform assign kar do

    public void ReturnDot(Dot dot)
    {
        dotPool.ReturnDotToPool(dot);
    }

    private int CalculateValueForDot()
    {
        // Placeholder for dot value calculation logic
        return Random.Range(1, 50001) * 2; // Example: Generates even values between 2 and 100,000
    }

    private Color CalculateColorForValue(int value)
    {
        // Placeholder for color determination logic
        // This method should map the value to a specific color
        return new Color(Random.value, Random.value, Random.value); // Example: Random color
    }
    
    private string GenerateDotName(int col, int row)
    {
        return string.Format("Dot ({0},{1})", col, row);
    }
}
