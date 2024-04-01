using UnityEngine;

public interface IDotFactory
{
    Dot GetDot(int col, int row);
    void ReturnDot(Dot dot);
}