using UnityEngine;

public interface IDotFactory
{
    Dot GetDot(Vector2 position);
    void ReturnDot(Dot dot);
}