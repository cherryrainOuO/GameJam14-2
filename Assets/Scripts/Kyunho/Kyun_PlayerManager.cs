using System.Collections.Generic;
using UnityEngine;

public class Kyun_PlayerManager : MonoBehaviour
{
    private const int WIDTH = 13;
    private const int HEIGHT = 11;
    private Kyun_IUnit[,] Map = new Kyun_IUnit[WIDTH, HEIGHT]; 

    [SerializeField] private Kyun_ChickenUnit player;
    [SerializeField] private Kyun_NatureEggSpriteIndicator eggSpriteIndicatorPrefab;
    [SerializeField] private Kyun_ChickenSpriteIndicator spriteIndicatorPrefab;
    [SerializeField] private float timer = 1.0f;

    private Kyun_NatureEggUnit egg;

    private List<Kyun_IUnit> units;
    private Kyun_DirectionType direction;
    private Kyun_DirectionType previousDirection;

    public float time = 1.0f;

    private void Awake()
    {
        units = new List<Kyun_IUnit>();
        var chickenSpriteIndicator = CreateSpriteIndicator(Kyun_UnitType.Chicken);
        var chicken = new Kyun_ChickenUnit(chickenSpriteIndicator);
        units.Add(chicken);

        previousDirection = Kyun_DirectionType.None;
        direction = Kyun_DirectionType.Down;
        chicken.Position = new Kyun_Coordinate(0, 0);
    }

    private void UpdateMap()
    {
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                Map[x, y] = null;
            }
        }

        foreach (var unit in units)
        {
            Map[unit.Position.X, unit.Position.Y] = unit; 
        }
    }

    private Kyun_ChickenSpriteIndicator CreateSpriteIndicator(Kyun_UnitType unitType)
    {
        switch(unitType)
        {
            case Kyun_UnitType.Chicken:
                return Instantiate(spriteIndicatorPrefab);
            case Kyun_UnitType.Chick:
                return Instantiate(spriteIndicatorPrefab);
        }
        return null;
    }

    public void Update()
    {
        UpdateDirection();
        UpdateEgg();
        if ((time -= Time.deltaTime) > 0) return;
        UpdatePreMove();
        UpdatePreEvent();
        UpdateUnitsBehaviour();
        time = timer;
    }

    private void UpdateEgg()
    {
        if (egg != null) return;
        var spriteIndicator = Instantiate(eggSpriteIndicatorPrefab);
        egg = new Kyun_NatureEggUnit(spriteIndicator);
        egg.Position = new Kyun_Coordinate(3, 3);
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
    }

    public void UpdatePreMove()
    {
        units[0].Direction = direction;
        units[0].Move();
    }

    private void UpdatePreEvent()
    {
        //var frontUnit = units[0].Direction;
        //if (frontUnit != null)
        //{
        //    switch (frontUnit.UnitType)
        //    {
        //        case Kyun_UnitType.NatureEgg:
        //            AddEgg();
        //            break;
        //        case Kyun_UnitType.Boss:
        //            Heading();
        //            break;
        //    }
        //}
    }

    private void UpdatePostEvent()
    {
        //var frontUnit = units[0].GetFrontUnit();
        //if (frontUnit != null)
        //{
        //    switch (frontUnit.UnitType)
        //    {
        //        case Kyun_UnitType.Chick:
        //            break;
        //        case Kyun_UnitType.Pork:
        //            break;
        //        case Kyun_UnitType.Egg:
        //            break;
        //    }
        //}
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
        //foreach (var unit in units)
        //{
        //    if (unit.UnitType == Kyun_UnitType.Chicken)
        //    {
        //        unit.Move(direction);
        //        previousDirection = direction;
        //    }
        //    else if (unit.UnitType == Kyun_UnitType.Chick || unit.UnitType == Kyun_UnitType.Egg)
        //    {
        //        unit.UpdateBehaviour();
        //    }
        //    else
        //    {
        //        unit.UpdateBehaviour();
        //    }
        //}
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
        //var lastUnit = units[units.Count - 1];
        //if (lastUnit.UnitType == Kyun_UnitType.Pork)
        //{
        //    var eggUnit = Instantiate(eggPrefab, player.transform.position, Quaternion.identity);
        //    units.Insert(units.Count - 2, eggUnit);
        //}
        //int index = 0;

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