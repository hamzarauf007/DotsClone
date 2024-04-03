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

    [HideInInspector]
    public int sumOfDots;
    [HideInInspector]
    public Color dotNewColor;
    
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
            
            dotNewColor =  dotsColorGenerator.sequence[key];
            sprite.color = dotNewColor;
            
            text.text = key.ToString();
            sumOfDots = key;
        }
    }

    public void DeActivate()
    {
        currentValues.Clear();
        obj.SetActive(false);
    }
}