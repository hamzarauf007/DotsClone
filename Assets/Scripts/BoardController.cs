using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UI;

public class BoardController : SingletonMonoBehaviour<BoardController>
{

    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int height = 5;
    
    
    [SerializeField]
    private LineRenderer currentPathDisplay = null;
    [SerializeField]
    private DotFactory dotFactory;
    [SerializeField]
    private DotPool dotPool;
    [SerializeField]
    private DisplayDot displayDot;
    
    private Dot[,] dotsCollection;

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        dotsCollection = new Dot[width, height];
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                dotsCollection[row, col] = CreateDot(col, row);
            }
        }

        currentPathDisplay.enabled = false;
        currentPathDisplay.positionCount = 0;
    }

    private Dot CreateDot(int col, int row)
    {
        return  dotFactory.GetDot(col, row);
    }

    public void UpdateDotSelection(Dot dot, List<Dot> currentDotPath)
    {
        if (currentDotPath.Count == 0)
        {
            SetPathDisplayColor(currentPathDisplay, dot.SpriteColor);
            SumOfDots(dot.DotValue, true);
            dot.Activate();
            currentDotPath.Add(dot);
        }
        else
        {
            Dot lastDot = currentDotPath[currentDotPath.Count - 1];

            // Check if the newly selected dot is the penultimate in the current path (for deselection)
            if (currentDotPath.Count > 1 && dot == currentDotPath[currentDotPath.Count - 2])
            {
                SumOfDots(dot.DotValue, false);
                // Deselect the last dot in the path
                lastDot.Deactivate();
                currentDotPath.RemoveAt(currentDotPath.Count - 1);
            }
            else
            {
                // Select a new dot if it's adjacent to the last dot and not already part of the path
                int lastDotX = lastDot.Column;
                int lastDotY = lastDot.Row;
                int newDotX = dot.Column;
                int newDotY = dot.Row;

                if (
                    lastDot != dot &&                                   // Not the same as the last dot
                    lastDot.DotValue == dot.DotValue &&                   // Same type of dot
                    !currentDotPath.Contains(dot) &&                    // Not already in the current path
                    Mathf.Abs(newDotX - lastDotX) <= 1 &&               // Only one step away on the board horizontally or diagonally
                    Mathf.Abs(newDotY - lastDotY) <= 1                  // Only one step away on the board vertically or diagonally
                )
                {
                    SumOfDots(dot.DotValue, true);
                    dot.Activate();
                    currentDotPath.Add(dot);
                }
            }
        }

        // Update the line renderer to reflect the current path
        currentPathDisplay.positionCount = currentDotPath.Count;
        currentPathDisplay.enabled = currentPathDisplay.positionCount > 0;
        currentPathDisplay.SetPositions(currentDotPath.Select(d => d.transform.position).ToArray());
    }

    private void SetPathDisplayColor(LineRenderer path,Color color)
    {
        Renderer renderer = path.GetComponent<Renderer>();
        Material mat = renderer.material;

     
        mat.EnableKeyword("_EMISSION");

        mat.SetColor("_EmissionColor", color);

        DynamicGI.SetEmissive(renderer, color);
    }

    public void FinalizeDotSelection(List<Dot> currentDotPath)
    {
        if (currentDotPath.Count > 1)
        {
            Dot lastDot = currentDotPath.Last();
            Vector3 lastDotPosition = lastDot.transform.position;
            
            int dotsToTween = currentDotPath.Count - 1; // Exclude the last dot from the count
            int tweensCompleted = 0;

            // Tween all dots in the path to the last dot's position
            foreach (var dot in currentDotPath)
            {
                if (dot != lastDot) // Skip the last dot
                {
                    StartCoroutine(UIHelpers.TweenPosition(dot.transform, lastDotPosition, 0.2f, () =>
                    {
                        tweensCompleted++;
                        dotPool.ReturnDotToPool(dot);
                        dotsCollection[dot.Row, dot.Column] = null;
                        
                        if (tweensCompleted == dotsToTween)
                        {
                            lastDot.SetDotValueColor(displayDot.sumOfDots, displayDot.dotNewColor);
                            // Once all dots have finished tweening, proceed to move remaining dots down after a delay
                            StartCoroutine(DelayedGridUpdate(0.2f)); // Wait for 1 second before starting grid update
                        }
                    }));
                }
            }
        }
        else
        {
            // If there was only one dot, simply deactivate it
            currentDotPath.ForEach(d => d.Deactivate());
        }

        // SumOfDots(0, true);
        DeActivateDot();
        // Reset the path display
        currentPathDisplay.enabled = false;
        currentPathDisplay.positionCount = 0;
        currentDotPath.Clear();
    }

    IEnumerator DelayedGridUpdate(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Move remaining dots down if needed
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height - 1; row++)
            {
                if (dotsCollection[row, col] == null) // Found an empty space
                {
                    // Look above to find the next non-null dot to move down
                    for (int i = row + 1; i < height; i++)
                    {
                        if (dotsCollection[i, col] != null)
                        {
                            // Move this dot down into the empty space
                            Dot dotToMove = dotsCollection[i, col];
                            dotsCollection[i, col] = null; // Remove from current position
                            dotsCollection[row, col] = dotToMove;
                            dotToMove.UpdateRow(row); // Update its row to the new position
                            dotToMove.gameObject.name = dotFactory.GenerateDotName(dotToMove.Column, dotToMove.Row);
                            
                            StartCoroutine(UIHelpers.TweenPosition(dotToMove.transform, new Vector2(col, row), 0.2f));
                            break; // Break since we've found a dot to move down into this space
                        }
                    }
                }
            }
        }

        // After moving dots down, generate and show new dots
        StartCoroutine(CreateAndShowNewDots(0.2f)); // Adjust delay as needed based on your game's behavior
    }
    
    IEnumerator CreateAndShowNewDots(float delay)
    { 
        yield return new WaitForSeconds(delay);
        for (int col = 0; col < width; col++)
        {
            for (int row = height - 1; row >= 0; row--) // Check from the top down
            {
                if (dotsCollection[row, col] == null)
                {
                    var newDot = CreateDot(col, row);
                    dotsCollection[row, col] = newDot;
                    // Position new dots just below the visible area, then move them up
                    newDot.transform.position = new Vector3(col, row); // Start below the board
                    StartCoroutine(UIHelpers.TweenPosition(newDot.transform, new Vector3(col, row), 0.2f));
                }
            }
        }
        yield return true;
    }

    private void SumOfDots(int value, bool adding)
    {
        displayDot.AdjustValueAndColor(value, adding);
    }

    private void DeActivateDot()
    {
        displayDot.DeActivate();
    }

}