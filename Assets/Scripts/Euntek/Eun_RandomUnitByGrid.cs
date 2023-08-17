using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eun_RandomUnitByGrid : MonoBehaviour
{
    [SerializeField] private LevelBase level;
    private GridBase grid;
    [SerializeField] private Unit_Chicken player;

    // Start is called before the first frame update
    void Start()
    {
        grid = level.GetGrid();

        //BossInit();

        //EggInit();
    }

    /// <summary>
    /// 에그 생성후 에그가 생성된 좌표 벡터를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public Vector2 EggInit()
    {
        int xMin = (int)Mathf.Clamp(player.Position.X - 5f, 0, grid.width - 1);
        int xMax = (int)Mathf.Clamp(player.Position.X + 5f, 0, grid.width - 1);
        int yMin = (int)Mathf.Clamp(player.Position.Y - 5f, 0, grid.height - 1);
        int yMax = (int)Mathf.Clamp(player.Position.Y + 5f, 0, grid.height - 1);

        while (true)
        {
            int randX = Random.Range(xMin, xMax);
            int randY = Random.Range(yMin, yMax);

            if (grid.GetValue(randX, randY) == (int)GridValue.GROUND_VALUE)
            //? 플레이어가 도달 못할수도 있음 -> 플레이어 주변 GridValue를 설정해서 거기서만 스폰되게 하자! 해결 완료
            {
                grid.SetValue(randX, randY, (int)GridValue.EGG_UNIT_VALUE);

                //Todo 에그 보이게 하기 
                // egg.transform.position = new Vector2(randX, randY);
                // egg.gameObject.SetActive(true);

                return new Vector2(randX, randY);
            }
        }
    }

    /// <summary>
    /// 보스 생성후 보스가 생성된 좌표를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public Vector2 BossInit()
    {
        int x = grid.width / 2;
        int y = grid.height / 2;

        grid.SetValue(x - 1, y + 1, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x, y + 1, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x + 1, y + 1, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x - 1, y, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x, y, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x + 1, y, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x - 1, y - 1, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x, y - 1, (int)GridValue.BOSS_UNIT_VALUE);
        grid.SetValue(x + 1, y - 1, (int)GridValue.BOSS_UNIT_VALUE);

        //Todo 보스 보이게 하기

        return new Vector2(x, y);
    }

}
