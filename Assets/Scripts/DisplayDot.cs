using UnityEngine;
using TMPro;

public class DisplayDot : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private DotsColorGenerator dotsColorGenerator;

    [SerializeField]
    private int sumValue = 0;
    
    private GameObject obj;

    private void Awake()
    {
        obj = this.gameObject;
    }

    public void CalculateColorAgainstValue(int value)
    {
        if (value != 0)
        {
            sumValue += value;
            obj.SetActive(true);
        }
        else
        {
            sumValue = 0;
            obj.SetActive(false);
            return;
        }
        
        // Placeholder for color determination logic
        // This method should map the value to a specific color
        if (dotsColorGenerator.sequence.ContainsKey(sumValue))
        {
            // Retrieve the value associated with the key
           sprite.color = dotsColorGenerator.sequence[sumValue];
           text.text = sumValue.ToString();
        }
    }
}
