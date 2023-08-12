using UnityEngine;

public class Kyun_ChickUnit : MonoBehaviour, Kyun_IUnit
{
    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType => Kyun_UnitType.Chick;
    public Vector3 PreviousUnitBeforePosition { get; private set; }
    public Vector3 PreviousUnitAfterPosition { get; private set; }

    public Kyun_IUnit GetFrontUnit()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateBehaviour()
    {
        throw new System.NotImplementedException();
    }
}