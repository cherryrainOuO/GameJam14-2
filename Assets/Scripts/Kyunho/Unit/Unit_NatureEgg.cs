public class Unit_NatureEgg : IUnit
{
    public IUnit PreviousUnit { get; set; }
    public IUnit NextUnit { get; set; }
    public UnitType UnitType => UnitType.NatureEgg;
    public ISpriteIndicator SpriteIndicator { get; }
    public DirectionType Direction { get; set; }
    public DirectionType LastDirection { get; private set; }

    public Coordinate LastPosition { get; private set; }
    public Coordinate Position { get;  set; }

    public Unit_NatureEgg(ISpriteIndicator spriteIndicator)
    {
        SpriteIndicator = spriteIndicator;
        SpriteIndicator.UpdateSprite(Direction, Position);
    }

    public IUnit GetFrontUnit()
    {
        return null;
    }

    public void Move() { }

    public void UpdateBehaviour() { }

    public void Destroy()
    {
        SpriteIndicator.DestroySprite();
    }

    public void Update()
    {
        SpriteIndicator.UpdateSprite(Direction, Position);
    }
}