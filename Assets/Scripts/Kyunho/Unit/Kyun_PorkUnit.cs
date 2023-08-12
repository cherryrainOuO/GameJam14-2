using UnityEngine;

public class Kyun_PorkUnit : MonoBehaviour, Kyun_IUnit
{
    private int coolTime = 0;
    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType { get; }
    public Vector3 PreviousUnitBeforePosition { get; private set; }
    public Vector3 PreviousUnitAfterPosition { get; private set; }

    public Kyun_IUnit GetFrontUnit()
    {
        return null;
    }

    public void Move(Vector3 direction)
    {
    }

    public void UpdateBehaviour()
    {
        if (coolTime > 0)
        {
            PreviousUnitBeforePosition = transform.position;
            transform.position = FollowingUnit.PreviousUnitBeforePosition;
            PreviousUnitAfterPosition = transform.position;
        }
        if (FollowingUnit.UnitType == Kyun_UnitType.Egg)
        {
            transform.position = FollowingUnit.PreviousUnitBeforePosition;
        }
    }
}