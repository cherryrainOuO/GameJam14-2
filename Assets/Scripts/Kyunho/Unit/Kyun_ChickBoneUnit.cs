﻿public class Kyun_ChickBoneUnit : Kyun_IUnit
{
    public Kyun_ChickBoneUnit(Kyun_ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
    }

    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType => Kyun_UnitType.Egg;
    public Kyun_ISpriteIndicator SpriteIndicator { get; }
    public Kyun_DirectionType Direction { get; set; }
    public Kyun_DirectionType LastDirection { get; private set; }

    public Kyun_Coordinate LastPosition { get; private set; }
    public Kyun_Coordinate Position { get; set; }

    public Kyun_IUnit GetFrontUnit()
    {
        return null;
    }

    public void Move()
    {
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