using UnityEngine;

public static class Kyun_ExtensionMethod
{
    public static Vector2 ToVector(this Kyun_DirectionType direction)
    {
        if (direction == Kyun_DirectionType.Up) return Vector2.up;
        if (direction == Kyun_DirectionType.Down) return Vector2.down;
        if (direction == Kyun_DirectionType.Left) return Vector2.left;
        if (direction == Kyun_DirectionType.Right) return Vector2.right;
        return Vector2.zero;
    }
}