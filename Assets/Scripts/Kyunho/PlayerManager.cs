using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int WIDTH = 13;
    private const int HEIGHT = 11;
    private IUnit[,] Map = new IUnit[WIDTH, HEIGHT];

    [SerializeField] private UnitFactory unitFactory;
    [SerializeField] private SceneSystem sceneSystem;
    [SerializeField] private float timer = 1.0f;

    private IUnit player;
    private IUnit natureEgg;
    private IUnit pork;

    private List<IUnit> units;
    private DirectionType direction;
    private DirectionType previousDirection;

    private bool isGameOver;
    public float time = 1.0f;
    private int porkCooldown = 0;

    private void Awake()
    {
        units = new List<IUnit>();
        player = CreateUnit(UnitType.Chicken);
        units.Add(player);

        previousDirection = DirectionType.Down;
        direction = DirectionType.Down;
        player.Position = new Coordinate(0, 0);
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

    private IUnit head;

    private void AddUnit(IUnit unit, UnitType type = UnitType.None)
    {
        var peekUnit = head;
        while (peekUnit.NextUnit != null)
        {
            if (peekUnit.NextUnit.UnitType == type)
            {
                break;
            }
            peekUnit = peekUnit.NextUnit;
        }
        peekUnit.NextUnit = unit;
    }

    private void RemoveUnit()
    {
        var peekUnit = head;
        while (peekUnit.NextUnit != null)
        {

        }
    }

    private ISpriteIndicator CreateSpriteIndicator(UnitType unitType)
    {
        return null;
    }

    private IUnit CreateUnit(UnitType unitType)
    {
        return unitFactory.CreateUnit(unitType);
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
            pork = CreateUnit(UnitType.Pork);
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
            var porkFollowingUnit = pork.PreviousUnit;
            if (porkFollowingUnit.UnitType == UnitType.Chicken)
            {
                SetGameOver();
            }
            else
            {
                if (porkFollowingUnit.UnitType == UnitType.Chick)
                {
                    porkCooldown += 25;
                }
                else
                {
                    porkCooldown += 15;
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
        Eun_SFXManager.Instance.SoundPlay((int)SFXSoundNumber.Attack1);
        StartCoroutine(sceneSystem.CoroutineForGameOver());
        isGameOver = true;
    }

    private void UpdateEgg()
    {
        if (natureEgg != null) return;
        natureEgg = CreateUnit(UnitType.NatureEgg);
        natureEgg.Position = GetRandomCoord();
        natureEgg.Update();
    }

    private Coordinate GetRandomCoord()
    {
        while (true)
        {
            int x = Random.Range(0, WIDTH);
            int y = Random.Range(0, HEIGHT);
            if (Map[x, y] == null) return new Coordinate(x, y);
        }
    }

    private void UpdateDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (previousDirection != DirectionType.Down)
            {
                direction = DirectionType.Up;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (previousDirection != DirectionType.Up)
            {
                direction = DirectionType.Down;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (previousDirection != DirectionType.Right)
            {
                direction = DirectionType.Left;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (previousDirection != DirectionType.Left)
            {
                direction = DirectionType.Right;
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
                if (unit.UnitType == UnitType.Egg)
                {
                    Eun_SFXManager.Instance.SoundPlay((int)SFXSoundNumber.Chick);
                    sceneSystem.ScoreUpdate(50);

                    int targetIndex = units.IndexOf(unit) - 1;
                    var chick = CreateUnit(UnitType.Chick);
                    chick.Position = unit.Position;
                    chick.Direction = unit.Direction;
                    chick.PreviousUnit = unit.PreviousUnit;
                    chick.Update();
                    unit.Destroy();
                    units.Remove(unit);
                    units.Insert(targetIndex, chick);
                }
                else if (unit.UnitType == UnitType.Pork || unit.UnitType == UnitType.Chick)
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
                Eun_SFXManager.Instance.SoundPlay((int)SFXSoundNumber.GetEgg);
                sceneSystem.ScoreUpdate(10);

                natureEgg.Destroy();
                natureEgg = null;
                AddEgg();
            }
        }
        UpdateFollow();
    }

    private void UpdateFollow()
    {
        IUnit frontUnit = null;
        for (int index = 0; index < units.Count; index++)
        {
            units[index].PreviousUnit = frontUnit;
            frontUnit = units[index];
        }
    }

    private void AddEgg()
    {
        var egg = CreateUnit(UnitType.Egg);
        var lastUnit = units[units.Count - 1];
        if (lastUnit.UnitType == UnitType.Pork)
        {
            units.Insert(units.Count - 2, egg);
        }
        else
        {
            units.Add(egg);
        }
        UpdateFollow();
        if (egg.PreviousUnit == null)
        {
            SetGameOver();
            return;
        }
        egg.Position = egg.PreviousUnit.LastPosition;
        egg.Update();
    }

    public void Heading()
    {
        for (int index = 0; index < units.Count; index++)
        {
            if (units[index].UnitType == UnitType.Chick)
            {
                var currentUnit = units[index];
                units.RemoveRange(0, index - 1);
                //currentUnit;
            }
        }
    }
}