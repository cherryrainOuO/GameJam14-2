public class Unit_Chick : IUnit
{
    public Unit_Chick(ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
    }

    public IUnit PreviousUnit { get; set; }
    public IUnit NextUnit { get; set; }
    public UnitType UnitType => UnitType.Chick;
    public ISpriteIndicator SpriteIndicator { get; }
    public DirectionType Direction { get; set; }
    public DirectionType LastDirection { get; private set; }

    public Coordinate LastPosition { get; private set; }
    public Coordinate Position { get;  set; }

    public void Move()
    {
        LastPosition = Position;
        LastDirection = Direction;
        Direction = PreviousUnit.LastDirection;
        Position = PreviousUnit.LastPosition;
        Update();
    }

    public IUnit GetFrontUnit()
    {
        return null;
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