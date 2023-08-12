public class Kyun_ChickenUnit : Kyun_IUnit
{
    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType => Kyun_UnitType.Chicken;
    public Kyun_ISpriteIndicator SpriteIndicator { get; }
    public Kyun_DirectionType Direction { get; set; }
    public Kyun_DirectionType LastDirection { get; private set; }

    public Kyun_Coordinate LastPosition { get; private set; }
    public Kyun_Coordinate Position { get; set; }

    public Kyun_ChickenUnit(Kyun_ISpriteIndicator spriteIndicator)
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

    public Kyun_IUnit GetFrontUnit()
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
