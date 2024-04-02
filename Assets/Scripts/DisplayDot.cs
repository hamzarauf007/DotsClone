using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class DisplayDot : MonoBehaviour
{
    [SerializeField] 
    private TextMeshPro text;
    [SerializeField] 
    private SpriteRenderer sprite;
    [SerializeField] 
    private GameObject obj;
    [SerializeField] 
    private DotsColorGenerator dotsColorGenerator;

    private int sumValue = 0;
    private List<int> currentValues = new List<int>(); // Tracks values of currently selected dots

    public void AdjustValueAndColor(int value, bool adding)
    {
        obj.SetActive(true);
        
        if (adding)
        {
            currentValues.Add(value);
        }
        else
        {
            currentValues.Remove(value);
        }

        UpdateSumAndColor();
    }

    private void UpdateSumAndColor()
    {
        sumValue = currentValues.Sum(); // Recalculate the sum

        if (sumValue > 0)
        {
            var key = dotsColorGenerator.sequence.Keys
                .Where(k => k <= sumValue)
                .DefaultIfEmpty(2)
                .Max(); // Find the highest key that is less than or equal to the sumValue
            
            sprite.color = dotsColorGenerator.sequence[key];
            text.text = key.ToString(); // Or key.ToString() if you want to display the found key
        }
    }

    public void DeActivate()
    {
        currentValues.Clear();
        obj.SetActive(false);
    }
}