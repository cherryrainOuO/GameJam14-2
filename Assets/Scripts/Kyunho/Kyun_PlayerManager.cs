using System.Collections.Generic;
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

    private Kyun_IUnit player;
    private Kyun_IUnit natureEgg;
    private Kyun_IUnit pork;

    private List<Kyun_IUnit> units;
    private Kyun_DirectionType direction;
    private Kyun_DirectionType previousDirection;

    [SerializeField] private SceneSystem sceneSystem;

    private bool isGameOver;
    public float time = 1.0f;
    private int porkCooldown = 0;

    private void Awake()
    {
        units = new List<Kyun_IUnit>();
        player = CreateUnit(Kyun_UnitType.Chicken);
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
            if (unit.Position.X < 0 || unit.Position.X >= WIDTH || unit.Position.Y < 0 || unit.Position.Y >= HEIGHT) continue;
            Map[unit.Position.X, unit.Position.Y] = unit;
        }
    }

    private Kyun_ISpriteIndicator CreateSpriteIndicator(Kyun_UnitType unitType)
    {
        switch (unitType)
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
        if (isGameOver) return;

        UpdateDirection();
        if ((time -= Time.deltaTime) > 0) return;
        UpdateMap();
        if (UpdateDeath()) return;
        UpdateEgg();
        UpdateMovement();
        UpdateEvent();
        UpdatePork();
        time = timer;
    }

    private bool IsOutOfMap()
    {
        return (player.Position.X < 0 || player.Position.X >= WIDTH || player.Position.Y < 0 || player.Position.Y >= HEIGHT);
    }

    private void UpdatePork()
    {
        if (pork == null && units.Count > 5)
        {
            pork = CreateUnit(Kyun_UnitType.Pork);
            pork.Position = units[units.Count - 1].LastPosition;
            pork.Update();
            units.Add(pork);
            UpdateFollow();
        }
        else if (pork != null)
        {
            if (porkCooldown > 0)
            {
                porkCooldown -= 1;
                return;
            }
            var porkFollowingUnit = pork.FollowingUnit;
            if (porkFollowingUnit.UnitType == Kyun_UnitType.Chicken)
            {
                SetGameOver();
            }
            else
            {
                if (porkFollowingUnit.UnitType == Kyun_UnitType.Chick)
                {
                    porkCooldown += 20;
                }
                else
                {
                    porkCooldown += 10;
                }
                units.Remove(porkFollowingUnit);
                porkFollowingUnit.Destroy();
                UpdateFollow();
            }
        }
    }

    private bool UpdateDeath()
    {
        if (IsOutOfMap())
        {
            SetGameOver();
            return true;
        }
        return false;
    }

    private void SetGameOver()
    {
        StartCoroutine(sceneSystem.CoroutineForGameOver());
        isGameOver = true;
    }

    private void UpdateEgg()
    {
        if (natureEgg != null) return;
        natureEgg = CreateUnit(Kyun_UnitType.NatureEgg);
        natureEgg.Position = GetRandomCoord();
        natureEgg.Update();
    }

    private Kyun_Coordinate GetRandomCoord()
    {
        while (true)
        {
            int x = Random.Range(0, WIDTH);
            int y = Random.Range(0, HEIGHT);
            if (Map[x, y] == null) return new Kyun_Coordinate(x, y);
        }
    }

    private void UpdateDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (previousDirection != Kyun_DirectionType.Down)
            {
                direction = Kyun_DirectionType.Up;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (previousDirection != Kyun_DirectionType.Up)
            {
                direction = Kyun_DirectionType.Down;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (previousDirection != Kyun_DirectionType.Right)
            {
                direction = Kyun_DirectionType.Left;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (previousDirection != Kyun_DirectionType.Left)
            {
                direction = Kyun_DirectionType.Right;
            }
        }
    }

    public void UpdateMovement()
    {
        previousDirection = player.Direction;
        player.Direction = direction;
        player.Move();

        for (int index = 1; index < units.Count; index++)
        {
            units[index].Move();
        }
    }

    private void UpdateEvent()
    {
        for (int index = 1; index < units.Count; index++)
        {
            var unit = units[index];
            if (player.Position == unit.Position)
            {
                if (unit.UnitType == Kyun_UnitType.Egg)
                {
                    int targetIndex = units.IndexOf(unit) - 1;
                    var chick = CreateUnit(Kyun_UnitType.Chick);
                    chick.Position = unit.Position;
                    chick.Direction = unit.Direction;
                    chick.FollowingUnit = unit.FollowingUnit;
                    chick.Update();
                    unit.Destroy();
                    units.Remove(unit);
                    units.Insert(targetIndex, chick);
                }
                else if (unit.UnitType == Kyun_UnitType.Pork || unit.UnitType == Kyun_UnitType.Chick)
                {
                    SetGameOver();
                    return;
                }
            }
        }

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
        for (int index = 0; index < units.Count; index++)
        {
            units[index].FollowingUnit = frontUnit;
            frontUnit = units[index];
        }
    }

    private void AddEgg()
    {
        var egg = CreateUnit(Kyun_UnitType.Egg);
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
        if (egg.FollowingUnit == null)
        {
            SetGameOver();
            return;
        }
        egg.Position = egg.FollowingUnit.LastPosition;
        egg.Update();
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