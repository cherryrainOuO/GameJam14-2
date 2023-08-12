﻿using System.Collections.Generic;
using UnityEngine;

public class Kyun_PlayerManager : MonoBehaviour
{
    private const int WIDTH = 13;
    private const int HEIGHT = 11;
    private Kyun_IUnit[,] Map = new Kyun_IUnit[WIDTH, HEIGHT]; 

    [SerializeField] private Kyun_ChickSpriteIndicator chickSpriteIndicatorPrefab;
    [SerializeField] private Kyun_ChickenSpriteIndicator chickenSpriteIndicatorPrefab;
    [SerializeField] private Kyun_ChickBoneSpriteIndicator chickBoneSpriteIndicatorPrefab;
    [SerializeField] private Kyun_NatureEggSpriteIndicator natureEggSpriteIndicatorPrefab;
    [SerializeField] private Kyun_EggSpriteIndicator eggSpriteIndicatorPrefab;
    [SerializeField] private Kyun_PorkSpriteIndicator porkSpriteIndicatorPrefab;
    [SerializeField] private float timer = 1.0f;

    private Kyun_ChickenUnit player;
    private Kyun_IUnit natureEgg;

    private List<Kyun_IUnit> units;
    private Kyun_DirectionType direction;
    private Kyun_DirectionType previousDirection;

    public float time = 1.0f;

    private void Awake()
    {
        units = new List<Kyun_IUnit>();
        var chickenSpriteIndicator = CreateSpriteIndicator(Kyun_UnitType.Chicken);
        player = new Kyun_ChickenUnit(chickenSpriteIndicator);
        units.Add(player);

        previousDirection = Kyun_DirectionType.Down;
        direction = Kyun_DirectionType.Down;
        player.Position = new Kyun_Coordinate(0, 0);
        player.Update();
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

    private Kyun_ISpriteIndicator CreateSpriteIndicator(Kyun_UnitType unitType)
    {
        switch(unitType)
        {
            case Kyun_UnitType.Chicken:
                return Instantiate(chickenSpriteIndicatorPrefab);
            case Kyun_UnitType.Chick:
                return Instantiate(chickSpriteIndicatorPrefab);
            case Kyun_UnitType.ChickBone:
                return Instantiate(chickBoneSpriteIndicatorPrefab);
            case Kyun_UnitType.Egg:
                return Instantiate(eggSpriteIndicatorPrefab);
            case Kyun_UnitType.NatureEgg:
                return Instantiate(natureEggSpriteIndicatorPrefab);
            case Kyun_UnitType.Pork:
                return Instantiate(porkSpriteIndicatorPrefab);
        }
        return null;
    }

    private Kyun_IUnit CreateUnit(Kyun_UnitType unitType)
    {
        Kyun_ISpriteIndicator spriteIndicator = CreateSpriteIndicator(unitType);
        switch (unitType)
        {
            case Kyun_UnitType.Chicken:
                return new Kyun_ChickenUnit(spriteIndicator);
            case Kyun_UnitType.Chick:
                return new Kyun_ChickUnit(spriteIndicator);
            case Kyun_UnitType.ChickBone:
                return new Kyun_ChickBoneUnit(spriteIndicator);
            case Kyun_UnitType.Egg:
                return new Kyun_EggUnit(spriteIndicator);
            case Kyun_UnitType.NatureEgg:
                return new Kyun_NatureEggUnit(spriteIndicator);
            case Kyun_UnitType.Pork:
                return new Kyun_PorkUnit(spriteIndicator);
        }
        return null;
    }

    public void Update()
    {
        UpdateDirection();
        if ((time -= Time.deltaTime) > 0) return;
        UpdateEgg();
        UpdatePreMove();
        UpdatePreEvent();
        UpdateUnitsBehaviour();
        time = timer;
    }

    private void UpdateEgg()
    {
        if (natureEgg != null) return;
        natureEgg = CreateUnit(Kyun_UnitType.NatureEgg);
        natureEgg.Position = new Kyun_Coordinate(4, 1);
        natureEgg.Update();
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
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (previousDirection != Kyun_DirectionType.Up)
            {
                direction = Kyun_DirectionType.Down;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (previousDirection != Kyun_DirectionType.Right)
            {
                direction = Kyun_DirectionType.Left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (previousDirection != Kyun_DirectionType.Left)
            {
                direction = Kyun_DirectionType.Right;
            }
        }
    }

    public void UpdatePreMove()
    {
        previousDirection = units[0].Direction;
        units[0].Direction = direction;
        units[0].Move();

        for (int index = 1; index < units.Count; index++)
        {
            units[index].Move();
        }
    }

    private void UpdatePreEvent()
    {
        //for (int index = 1; index < units.Count; index++)
        //{
        //    var unit = units[index];
        //    if (player.Position == unit.Position)
        //    {
        //        if (unit.UnitType == Kyun_UnitType.Egg)
        //        {
        //            int targetIndex = units.IndexOf(unit) - 1;
        //            var chick = CreateUnit(Kyun_UnitType.Chick);
        //            chick.Position = unit.Position;
        //            chick.Direction = unit.Direction;
        //            chick.FollowingUnit = unit.FollowingUnit;
        //            chick.Update();
        //            unit.Destroy();
        //            units.Remove(unit);
        //            units.Insert(targetIndex, chick);
        //        }
        //    }
        //}

        if (natureEgg != null)
        {
            if (player.Position == natureEgg.Position)
            {
                natureEgg.Destroy();
                natureEgg = null;
                AddEgg();
            }
        }
        UpdateFollow();
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

    private void AddEgg()
    {
        var egg = CreateUnit(Kyun_UnitType.Egg);
        egg.Position = player.Position;
        egg.Update();
        var lastUnit = units[units.Count - 1];
        if (lastUnit.UnitType == Kyun_UnitType.Pork)
        {
            units.Insert(units.Count - 2, egg);
        }
        else
        {
            units.Add(egg);
        }
        UpdateFollow();
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