public class Kyun_PorkUnit : Kyun_IUnit
{
    private int coolTime = 0;

    public Kyun_PorkUnit(Kyun_ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
    }

    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType => Kyun_UnitType.Pork;
    public Kyun_ISpriteIndicator SpriteIndicator { get; }
    public Kyun_DirectionType Direction { get; set; }
    public Kyun_DirectionType LastDirection { get; private set; }

    public Kyun_Coordinate LastPosition { get; private set; }
    public Kyun_Coordinate Position { get;  set; }

    public Kyun_IUnit GetFrontUnit()
    {
        return null;
    }

    public void Move()
    {
        LastPosition = Position;
        LastDirection = Direction;
        Direction = FollowingUnit.LastDirection;
        Position = FollowingUnit.LastPosition;
        Update();
    }

    public void UpdateBehaviour()
    {
        //if (coolTime > 0)
        //{
        //    LastPosition = transform.position;
        //    transform.position = FollowingUnit.LastPosition;
        //    PreviousUnitAfterPosition = transform.position;
        //}
        //if (FollowingUnit.UnitType == Kyun_UnitType.Egg)
        //{
        //    transform.position = FollowingUnit.LastPosition;
        //}
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