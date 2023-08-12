using UnityEngine;

public class Kyun_NatureEggUnit : MonoBehaviour, Kyun_IUnit
{
    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType => Kyun_UnitType.NatureEgg;
    public Vector3 PreviousUnitBeforePosition { get; private set; }
    public Vector3 PreviousUnitAfterPosition { get; private set; }

    public Kyun_IUnit GetFrontUnit()
    {
        return null;
    }

    public void Move(Vector3 direction) { }

    public void UpdateBehaviour() { }
}