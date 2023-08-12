using System.Collections.Generic;
using UnityEngine;

public class Kyun_PlayerManager : MonoBehaviour
{
    [SerializeField] private Kyun_ChickenUnit player;
    [SerializeField] private Kyun_EggUnit eggPrefab;

    public List<Kyun_IUnit> units;
    private Kyun_DirectionType direction;
    private Kyun_DirectionType previousDirection;

    public float time = 1.0f;

    private void Awake()
    {
        units = new List<Kyun_IUnit>();
        units.Add(player);

        previousDirection = Kyun_DirectionType.None;
        direction = Kyun_DirectionType.None;
    }

    private void UpdateDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction != Kyun_DirectionType.Down)
            {
            direction = Kyun_DirectionType.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction != Kyun_DirectionType.Up)
            {
                direction = Kyun_DirectionType.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction != Kyun_DirectionType.Right)
            {
                direction = Kyun_DirectionType.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction != Kyun_DirectionType.Left)
            {
                direction = Kyun_DirectionType.Right;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateUnitAndAdd();
        }
    }

    public void Update()
    {
        UpdateDirection();
        if ((time -= Time.deltaTime) > 0) return;
        UpdateUnits();
        time = 0.1f;
    }

    private void CreateUnitAndAdd()
    {
        var eggUnit = Instantiate(eggPrefab);
        AddUnitToPlayer(eggUnit);
    }

    public void UpdateUnits()
    {
        foreach (var unit in units)
        {
            if (unit.UnitType == Kyun_UnitType.Chicken)
            {
                unit.Move(direction.ToVector());
                previousDirection = direction;
            }
            else if (unit.UnitType == Kyun_UnitType.Chick || unit.UnitType == Kyun_UnitType.Egg)
            {
                unit.UpdateBehaviour();
            }
            else
            {
                unit.UpdateBehaviour();
            }
        }
    }

    public void AddUnitToPlayer(Kyun_IUnit unit)
    {
        var lastUnit = units[units.Count - 1];
        unit.FollowingUnit = lastUnit;
        units.Add(unit);
    }

    public void Heading()
    {

    }
}
