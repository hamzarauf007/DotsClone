using System.Collections.Generic;
using UnityEngine;

public class InputController : SingletonMonoBehaviour<InputController>
{
    private float touchAngle = 0;
    private bool isDragging = false;

    private List<Dot> currentDotPath = new List<Dot>();

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnFinishedTouch();
        }
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        if (isDragging)
        {
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, 100);
            if (hit2D.collider)
            {
                var dot = hit2D.transform.GetComponent<Dot>();
                OnDotTouched(dot);
            }
        }
    }

    private void OnFinishedTouch()
    {
        isDragging = false;
        BoardController.Instance.FinalizeDotSelection(currentDotPath);
        currentDotPath.Clear();
    }

    private void OnDotTouched(Dot dot)
    {
        BoardController.Instance.UpdateDotSelection(dot, currentDotPath);
    }

}
