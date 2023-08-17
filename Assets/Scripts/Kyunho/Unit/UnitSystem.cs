using System;

public enum UnitType
{
    Chicken,
    Chick,
    Egg,
    NatureEgg,
    Pork,
    Boss,
    ChickBone,
    None
}

public enum DirectionType
{
    None,
    Up,
    Down,
    Left,
    Right
}

public struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        return obj is Coordinate coordinate &&
               X == coordinate.X &&
               Y == coordinate.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static Coordinate operator +(Coordinate k1, Coordinate k2)
    {
        return new Coordinate(k1.X + k2.X, k1.Y + k2.Y);
    }

    public static Coordinate operator -(Coordinate k1, Coordinate k2)
    {
        return new Coordinate(k1.X - k2.X, k1.Y - k2.Y);
    }

    public static bool operator ==(Coordinate k1, Coordinate k2)
    {
        return k1.X == k2.X && k1.Y == k2.Y;
    }

    public static bool operator !=(Coordinate k1, Coordinate k2)
    {
        return k1.X != k2.X || k1.Y != k2.Y;
    }
}

public interface IUnit
{
    ISpriteIndicator SpriteIndicator { get; }
    IUnit PreviousUnit { get; set; }
    IUnit NextUnit { get; set; }
    UnitType UnitType { get; }
    DirectionType LastDirection { get; }
    DirectionType Direction { get; set; }
    Coordinate LastPosition { get; }
    Coordinate Position { get; set; }

    void Move();
    void Destroy();
    void UpdateBehaviour();
    void Update();
}

public interface ISpriteIndicator
{
    void UpdateSprite(DirectionType direction, Coordinate coordinate);
    void DestroySprite();
}