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

    public static Kyun_Coordinate ToCoordinate(this Kyun_DirectionType direction)
    {
        if (direction == Kyun_DirectionType.Up) return new Kyun_Coordinate(0, -1);
        if (direction == Kyun_DirectionType.Down) return new Kyun_Coordinate(0, 1);
        if (direction == Kyun_DirectionType.Left) return new Kyun_Coordinate(-1, 0);
        if (direction == Kyun_DirectionType.Right) return new Kyun_Coordinate(1, 0);
        return new Kyun_Coordinate(0, 0);
    }

    public static Kyun_DirectionType ToNegative(this Kyun_DirectionType direction)
    {
        if (direction == Kyun_DirectionType.Up) return Kyun_DirectionType.Down;
        if (direction == Kyun_DirectionType.Down) return Kyun_DirectionType.Up;
        if (direction == Kyun_DirectionType.Left) return Kyun_DirectionType.Left;
        if (direction == Kyun_DirectionType.Right) return Kyun_DirectionType.Right;
        return Kyun_DirectionType.None;
    }

    public static string ToAnimatorString(this Kyun_DirectionType direction)
    {
        if (direction == Kyun_DirectionType.Up) return "Up";
        if (direction == Kyun_DirectionType.Down) return "Down";
        if (direction == Kyun_DirectionType.Left) return "Left";
        if (direction == Kyun_DirectionType.Right) return "Right";
        return "";
    }

    public static void InitializeAnimator(this Animator animator, Kyun_DirectionType exception)
    {
        if (exception != Kyun_DirectionType.Up) animator.SetBool(Kyun_DirectionType.Up.ToAnimatorString(), false);
        if (exception != Kyun_DirectionType.Down) animator.SetBool(Kyun_DirectionType.Down.ToAnimatorString(), false);
        if (exception != Kyun_DirectionType.Left) animator.SetBool(Kyun_DirectionType.Left.ToAnimatorString(), false);
        if (exception != Kyun_DirectionType.Right) animator.SetBool(Kyun_DirectionType.Right.ToAnimatorString(), false);
    }

    public static Vector3 ToVector(this Kyun_Coordinate coordinate)
    {
        return new Vector3(coordinate.X - 6, -coordinate.Y + 4.5f, 0);
    }
}