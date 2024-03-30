using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UI;

/// <summary>
/// Board controller - manages the game logic and the overall board state & display
/// </summary>
public class BoardController : SingletonMonoBehaviour<BoardController>
{

    public int width = 5;
    public int height = 5;

    public GameObject[] dotPrefabs = null;

    public LineRenderer currentPathDisplay = null;

    private Dot[,] dotsCollection;

    void Start()
    {
        Setup();
    }

    void Update()
    {

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

    private string GenerateDotName(int col, int row)
    {
        return string.Format("Dot ({0},{1})", col, row);
    }

    private Dot CreateDot(int col, int row)
    {
        // select a type of dot at random from our dot prefabs
        int typeOfDot = UnityEngine.Random.Range(0, dotPrefabs.Length);
        Vector2 tmpPos = new Vector2(col, row);
        GameObject newDot = Instantiate<GameObject>(dotPrefabs[typeOfDot], tmpPos, Quaternion.identity);
        newDot.gameObject.name = GenerateDotName(col, row);
        newDot.transform.parent = this.transform;
        Dot dotCtl = newDot.GetComponent<Dot>();
        // setup the dot properties
        dotCtl.Setup(typeOfDot, row, col);
        return dotCtl;
    }

    public void UpdateDotSelection(Dot dot, List<Dot> currentDotPath)
    {
        if (currentDotPath.Count == 0)
        {
            // Highlight a new dot
            dot.Activate();
            currentDotPath.Add(dot);
        }
        else
        {
            // Highlight a new dot on the path if the dot is selectable according to the game rules
            Dot lastDot = currentDotPath[currentDotPath.Count - 1];
            int lastDotX = lastDot.Column;
            int lastDotY = lastDot.Row;
            int newDotX = dot.Column;
            int newDotY = dot.Row;
            if (
                lastDot != dot &&                                   // Not the same as the last dot
                lastDot.DotType == dot.DotType &&                   // Same type of dot
                !currentDotPath.Contains(dot) &&                    // Not already in the current path
                Mathf.Abs(newDotX - lastDotX) <= 1 &&               // Only one step away on the board horizontally or diagonally
                Mathf.Abs(newDotY - lastDotY) <= 1                  // Only one step away on the board vertically or diagonally
            )
            {
                dot.Activate();
                currentDotPath.Add(dot);
            }
        }

        // Display the path as connected lines
        currentPathDisplay.positionCount = currentDotPath.Count;
        currentPathDisplay.enabled = currentPathDisplay.positionCount > 0;
        currentPathDisplay.SetPositions(currentDotPath.Select(d => d.transform.position).ToArray());
    }


    public void FinalizeDotSelection(List<Dot> currentDotPath)
    {
        if (currentDotPath.Count > 1)
        {
            bool hasTransitioningDots = false;
            foreach (var dot in currentDotPath.OrderBy(d => -d.Row)) // Invert the order
            {
                dotsCollection[dot.Row, dot.Column] = null;
                for (int i = dot.Row; i < height; i++) // Start moving dots downwards, not upwards
                {
                    var aboveDot = (i + 1 < height) ? dotsCollection[i + 1, dot.Column] : null;
                    if (aboveDot != null && !currentDotPath.Contains(aboveDot))
                    {
                        dotsCollection[i + 1, dot.Column] = null;
                        aboveDot.Row -= 1; // Move down, not up
                        dotsCollection[aboveDot.Row, aboveDot.Column] = aboveDot;
                        aboveDot.gameObject.name = GenerateDotName(aboveDot.Column, aboveDot.Row);

                        StartCoroutine(UIHelpers.TweenPosition(aboveDot.transform, new Vector2(aboveDot.Column, aboveDot.Row), 0.3f));
                        hasTransitioningDots = true;
                    }
                }
            }
            StartCoroutine(CreateAndShowNewDots(hasTransitioningDots ? 0.3f : 0.3f));
            currentDotPath.ForEach(d => Destroy(d.gameObject));
        }
        else
        {
            currentDotPath.ForEach(d => d.Deactivate());
        }

        currentPathDisplay.enabled = false;
        currentPathDisplay.positionCount = 0;
        currentDotPath.Clear();
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
}