using System;

public enum Kyun_UnitType
{
    Chicken,
    Chick,
    Egg,
    NatureEgg,
    Pork,
    Boss,
    ChickBone
}

public enum Kyun_DirectionType
{
    None,
    Up,
    Down,
    Left,
    Right
}

public struct Kyun_Coordinate
{
    public int X;
    public int Y;

    public Kyun_Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        return obj is Kyun_Coordinate coordinate &&
               X == coordinate.X &&
               Y == coordinate.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static Kyun_Coordinate operator +(Kyun_Coordinate k1, Kyun_Coordinate k2)
    {
        return new Kyun_Coordinate(k1.X + k2.X, k1.Y + k2.Y);
    }

    public static Kyun_Coordinate operator -(Kyun_Coordinate k1, Kyun_Coordinate k2)
    {
        return new Kyun_Coordinate(k1.X - k2.X, k1.Y - k2.Y);
    }

    public static bool operator ==(Kyun_Coordinate k1, Kyun_Coordinate k2)
    {
        return k1.X == k2.X && k1.Y == k2.Y;
    }

    public static bool operator !=(Kyun_Coordinate k1, Kyun_Coordinate k2)
    {
        return k1.X != k2.X || k1.Y != k2.Y;
    }
}

public interface Kyun_IUnit
{
    Kyun_ISpriteIndicator SpriteIndicator { get; }
    Kyun_IUnit FollowingUnit { get; set; }
    Kyun_UnitType UnitType { get; }
    Kyun_DirectionType LastDirection { get; }
    Kyun_DirectionType Direction { get; set; }
    Kyun_Coordinate LastPosition { get; }
    Kyun_Coordinate Position { get; set; }

    void Move();
    void Destroy();
    void UpdateBehaviour();
    void Update();
}

public interface Kyun_ISpriteIndicator
{
    void UpdateSprite(Kyun_DirectionType direction, Kyun_Coordinate coordinate);
    void DestroySprite();
}