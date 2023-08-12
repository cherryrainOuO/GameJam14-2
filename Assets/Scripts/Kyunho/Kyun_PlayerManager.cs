using System.Collections.Generic;
using UnityEngine;

public class Kyun_PlayerManager : MonoBehaviour
{
    [SerializeField] private Kyun_ChickenUnit player;
    [SerializeField] private Kyun_EggUnit eggPrefab;

    private List<Kyun_IUnit> units;
    private Kyun_DirectionType direction;
    private Kyun_DirectionType previousDirection;

    public float time = 1.0f;
    [SerializeField] private float timer = 1.0f;

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
            if (previousDirection != Kyun_DirectionType.Down)
            {
                direction = Kyun_DirectionType.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (previousDirection != Kyun_DirectionType.Up)
            {
                direction = Kyun_DirectionType.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (previousDirection != Kyun_DirectionType.Right)
            {
                direction = Kyun_DirectionType.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (previousDirection != Kyun_DirectionType.Left)
            {
                direction = Kyun_DirectionType.Right;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }
    }

    public void Update()
    {
        UpdateDirection();
        if ((time -= Time.deltaTime) > 0) return;
        UpdatePreEvent();
        UpdateUnitsBehaviour();
        time = timer;
    }

    private void UpdatePreEvent()
    {
        var frontUnit = units[0].GetFrontUnit();
        if (frontUnit != null)
        {
            switch (frontUnit.UnitType)
            {
                case Kyun_UnitType.NatureEgg:
                    AddEgg();
                    break;
                case Kyun_UnitType.Boss:
                    Heading();
                    break;
            }
        }
    }

    private void UpdatePostEvent()
    {
        var frontUnit = units[0].GetFrontUnit();
        if (frontUnit != null)
        {
            switch (frontUnit.UnitType)
            {
                case Kyun_UnitType.Chick:
                    break;
                case Kyun_UnitType.Pork:
                    break;
                case Kyun_UnitType.Egg:
                    break;
            }
        }
    }

    private void UpdateFollow()
    {
        Kyun_IUnit frontUnit = null;
        foreach (var unit in units)
        {
            unit.FollowingUnit = frontUnit;
            frontUnit = unit;
        }
    }

    public void UpdateUnitsBehaviour()
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

    public void RemoveUnit(Kyun_IUnit unit)
    {
        switch (unit.UnitType)
        {
            case Kyun_UnitType.Egg:
                break;
            case Kyun_UnitType.Chick:
                break;
        }
        if (unit.UnitType == Kyun_UnitType.Egg)
        {
            units.Remove(unit);
        }
        UpdateFollow();
    }

    public void AddUnit(Kyun_IUnit unit)
    {
        units.Add(unit);
        UpdateFollow();
    }

    private void AddEgg()
    {
        var lastUnit = units[units.Count - 1];
        if (lastUnit.UnitType == Kyun_UnitType.Pork)
        {
            var eggUnit = Instantiate(eggPrefab, player.transform.position, Quaternion.identity);
            units.Insert(units.Count - 2, eggUnit);
        }
        int index = 0;

        //units.Insert()
        //unit.FollowingUnit = lastUnit;
        //units.Add(unit);
    }

    public void Heading()
    {
        for (int index = 0; index < units.Count; index++)
        {
            if (units[index].UnitType == Kyun_UnitType.Chick)
            {
                var currentUnit = units[index];
                units.RemoveRange(0, index - 1);
                //currentUnit;
            }
        }
    }
}