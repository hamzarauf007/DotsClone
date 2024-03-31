using UnityEngine;

public class DotFactory : IDotFactory
{
    public DotPool dotPool;
    

    public Dot GetDot(Vector2 position)
    {
        // Retrieve a dot from the pool and set its position
        Dot dot = dotPool.GetDot();
        dot.transform.position = position;
        // Here, you could also set the dot's value and color
        int value = CalculateValueForDot(); // Implement this method based on your game logic
        Color color = CalculateColorForValue(value); // Implement this method based on your game logic
        // dot.Initialize(value, color);
        return dot;
    }

    public Dot GetDot(System.Numerics.Vector2 position)
    {
        throw new System.NotImplementedException();
    }

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
}
