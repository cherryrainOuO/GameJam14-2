using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eun_RandomUnitByGrid : MonoBehaviour
{
    [SerializeField] private LevelBase level;
    private GridBase grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = level.GetGrid();

        Init();
    }

    private void Init()
    {
        while (true)
        {
            int randX = Random.Range(0, grid.width);
            int randY = Random.Range(0, grid.height);

            if (grid.GetValue(randX, randY) == (int)GridValue.GROUND_VALUE)
            //! 단점 : 플레이어가 도달 못할수도 있음 -> 플레이어 주변 GridValue를 설정해서 거기서만 스폰되게 하자!
            {
                grid.SetValue(randX, randY, (int)GridValue.EGG_UNIT_VALUE);
                break;
            }
        }

    }

}
