public class Unit_Chicken : IUnit
{
    public IUnit PreviousUnit { get; set; }
    public IUnit NextUnit { get; set; }
    public UnitType UnitType => UnitType.Chicken;
    public ISpriteIndicator SpriteIndicator { get; }
    public DirectionType Direction { get; set; }
    public DirectionType LastDirection { get; private set; }

    public Coordinate LastPosition { get; private set; }
    public Coordinate Position { get; set; }

    public Unit_Chicken(ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
    }

    public void Move()
    {
        LastPosition = Position;
        LastDirection = Direction;
        Position += Direction.ToCoordinate();
        Update();
    }

    public void UpdateBehaviour()
    {
        //FollowingUnit.
    }

    public IUnit GetFrontUnit()
    {
        return null;
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
