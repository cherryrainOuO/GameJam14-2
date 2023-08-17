using UnityEngine;

public static class Kyun_ExtensionMethod
{
    public static Vector2 ToVector(this DirectionType direction)
    {
        if (direction == DirectionType.Up) return Vector2.up;
        if (direction == DirectionType.Down) return Vector2.down;
        if (direction == DirectionType.Left) return Vector2.left;
        if (direction == DirectionType.Right) return Vector2.right;
        return Vector2.zero;
    }

    public static Coordinate ToCoordinate(this DirectionType direction)
    {
        if (direction == DirectionType.Up) return new Coordinate(0, -1);
        if (direction == DirectionType.Down) return new Coordinate(0, 1);
        if (direction == DirectionType.Left) return new Coordinate(-1, 0);
        if (direction == DirectionType.Right) return new Coordinate(1, 0);
        return new Coordinate(0, 0);
    }

    public static DirectionType ToNegative(this DirectionType direction)
    {
        if (direction == DirectionType.Up) return DirectionType.Down;
        if (direction == DirectionType.Down) return DirectionType.Up;
        if (direction == DirectionType.Left) return DirectionType.Left;
        if (direction == DirectionType.Right) return DirectionType.Right;
        return DirectionType.None;
    }

    public static string ToAnimatorString(this DirectionType direction)
    {
        if (direction == DirectionType.Up) return "Up";
        if (direction == DirectionType.Down) return "Down";
        if (direction == DirectionType.Left) return "Left";
        if (direction == DirectionType.Right) return "Right";
        return "";
    }

    public static void InitializeAnimator(this Animator animator, DirectionType exception)
    {
        if (exception != DirectionType.Up) animator.SetBool(DirectionType.Up.ToAnimatorString(), false);
        if (exception != DirectionType.Down) animator.SetBool(DirectionType.Down.ToAnimatorString(), false);
        if (exception != DirectionType.Left) animator.SetBool(DirectionType.Left.ToAnimatorString(), false);
        if (exception != DirectionType.Right) animator.SetBool(DirectionType.Right.ToAnimatorString(), false);
    }

    public static Vector3 ToVector(this Coordinate coordinate)
    {
        return new Vector3(coordinate.X - 6, -coordinate.Y + 4.5f, 0);
    }
}