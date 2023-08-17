using UnityEngine;

public class Unit_Pork : IUnit
{
    public int CoolTime = 0;

    public Unit_Pork(ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
    }

    public IUnit PreviousUnit { get; set; }
    public IUnit NextUnit { get; set; }

    public UnitType UnitType => UnitType.Pork;
    public ISpriteIndicator SpriteIndicator { get; }
    public DirectionType Direction { get; set; }
    public DirectionType LastDirection { get; private set; }

    public Coordinate LastPosition { get; private set; }
    public Coordinate Position { get;  set; }

    public IUnit GetFrontUnit()
    {
        return null;
    }

    public void Move()
    {
        LastPosition = Position;
        LastDirection = Direction;
        Debug.Log(PreviousUnit.UnitType);
        Direction = PreviousUnit.LastDirection;
        Position = PreviousUnit.LastPosition;
        Update();
    }

    public void UpdateBehaviour()
    {
    }

    public void Destroy()
    {
        SpriteIndicator.DestroySprite();
    }

    public void Update()
    {
        SpriteIndicator.UpdateSprite(Direction, Position);
    }
}