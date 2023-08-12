public class Kyun_ChickUnit : Kyun_IUnit
{
    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType => Kyun_UnitType.Chick;
    public Kyun_ISpriteIndicator SpriteIndicator { get; }
    public Kyun_DirectionType Direction { get; set; }
    public Kyun_DirectionType LastDirection { get; private set; }

    public Kyun_Coordinate LastPosition { get; private set; }
    public Kyun_Coordinate Position { get;  set; }

    public void Move()
    {
        LastPosition = Position;
        LastDirection = Direction;
        Direction = FollowingUnit.LastDirection;
        Position += Direction.ToCoordinate();
    }

    public Kyun_IUnit GetFrontUnit()
    {
        return null;
    }

    public void UpdateBehaviour()
    {

    }
}