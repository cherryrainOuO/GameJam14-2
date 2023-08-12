using System.Collections.Generic;
using UnityEngine;

public class Kyun_UnitSystem : MonoBehaviour
{
    private List<Kyun_IUnit> units;

    public void UpdateUnits()
    {
        foreach (var unit in units)
        {
            if (unit.UnitType == Kyun_UnitType.Chick)
            {
                var frontUnit = unit.GetFrontUnit();
            }
            unit.UpdateBehaviour();
        }
    }
}

public enum Kyun_UnitType { Chicken, Chick, Egg, Pork }
public enum Kyun_DirectionType { None, Up, Down, Left, Right }
public class Kyun_Coordinate
{
    public int X;
    public int Y;
}

public interface Kyun_IUnit
{
    Kyun_IUnit FollowingUnit { get; set; }
    Kyun_UnitType UnitType { get; }
    Vector3 PreviousUnitBeforePosition { get; }
    Vector3 PreviousUnitAfterPosition { get; }

    void Move(Vector3 direction);
    void UpdateBehaviour();
    //void MoveTo(Vector2 position);
    Kyun_IUnit GetFrontUnit();
    //Vector2 Position { get; }
}