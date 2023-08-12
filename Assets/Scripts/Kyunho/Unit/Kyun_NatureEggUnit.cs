public class Kyun_NatureEggUnit : Kyun_IUnit
{
    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType => Kyun_UnitType.NatureEgg;
    public Kyun_ISpriteIndicator SpriteIndicator { get; }
    public Kyun_DirectionType Direction { get; set; }
    public Kyun_DirectionType LastDirection { get; private set; }

    public Kyun_Coordinate LastPosition { get; private set; }
    public Kyun_Coordinate Position { get;  set; }

    public Kyun_NatureEggUnit(Kyun_ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
        SpriteIndicator.UpdateSprite(Direction, Position);
    }

    public Kyun_IUnit GetFrontUnit()
    {
        return null;
    }

    public void Move() { }

    public void UpdateBehaviour() { }
}