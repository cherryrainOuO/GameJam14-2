using UnityEngine;

public class Kyun_ChickenUnit : MonoBehaviour, Kyun_IUnit
{
    public Kyun_IUnit FollowingUnit { get; set; }
    public Kyun_UnitType UnitType { get; }
    public Kyun_DirectionType Direction { get; }

    public Vector3 PreviousUnitBeforePosition { get; private set; }
    public Vector3 PreviousUnitAfterPosition { get; private set; }

    public void Move(Vector3 direction)
    {
        PreviousUnitBeforePosition = transform.position;
        transform.position += direction;
        PreviousUnitAfterPosition = transform.position;
    }

    public Vector3 Position { get => transform.position; }

    public void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateBehaviour()
    {
        //FollowingUnit.
    }

    public Kyun_IUnit GetFrontUnit()
    {
        return null;
        //int count = Physics2D.RaycastNonAlloc(transform.position, )
    }
}
