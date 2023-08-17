public class Unit_Egg : IUnit
{
    public Unit_Egg(ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
    }

    public IUnit PreviousUnit { get; set; }
    public IUnit NextUnit { get; set; }
    public UnitType UnitType => UnitType.Egg;
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
        Direction = PreviousUnit.LastDirection;
        Position = PreviousUnit.LastPosition;
        Update();
    }

    public void UpdateBehaviour()
    {
        //LastPosition = transform.position;
        //transform.position = FollowingUnit.LastPosition;
        //PreviousUnitAfterPosition = transform.position;
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
