using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DotFactory : MonoBehaviour, IDotFactory
{
    [SerializeField]
    private Transform board;
    [SerializeField]
    private DotPool dotPool;
    [SerializeField]
    private DotsColorGenerator dotsColorGenerator;
    [SerializeField]
    private int currentMaxSpawnValue = 2;
    
    private int complexityNumber;
    
    public Dot GetDot(int col, int row)
    {
        // Retrieve a dot from the pool and set its position
        Dot dot = dotPool.GetDot();
        dot.transform.position = new Vector2(col, row);
        dot.transform.parent = board;
        dot.gameObject.name = GenerateDotName(col, row);
        int value = CalculateValueForDot();
        Color çolor = CalculateColorForValue(value);
        dot.InitialSetup(value, row, col, çolor);
        OnMissionComplete();
        return dot;
    }

    public void ReturnDot(Dot dot)
    {
        dotPool.ReturnDotToPool(dot);
    }

    private int CalculateValueForDot()
    {
        // Filter the keys to include only those less than or equal to currentMaxSpawnValue
        var possibleValues = dotsColorGenerator.sequence.Keys.Where(k => k <= currentMaxSpawnValue).ToList();

        // Randomly select a value from these keys
        int randomIndex = Random.Range(0, possibleValues.Count);
        randomIndex = possibleValues[randomIndex];
        return randomIndex;
    }

    private Color CalculateColorForValue(int value)
    {
        // Placeholder for color determination logic
        // This method should map the value to a specific color
        if (dotsColorGenerator.sequence.ContainsKey(value))
        {
            // Retrieve the value associated with the key
            return dotsColorGenerator.sequence[value];
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

    // Mocking the mission complete logic
    private void OnMissionComplete()
    {
        complexityNumber++;
        if (complexityNumber % 60 == 0)
        {
            IncreaseSpawnValue();
        }
    }

    private void IncreaseSpawnValue()
    {
        Debug.LogError("IncreaseSpawnValue");
        var nextValue = dotsColorGenerator.sequence.Keys.FirstOrDefault(k => k > currentMaxSpawnValue);
        if (nextValue != 0) 
        {
            currentMaxSpawnValue = nextValue;
        }
    }
}
