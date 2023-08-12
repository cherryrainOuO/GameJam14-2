using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelBase : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    private float cellSize = 1f;
    private Vector3 originPosition;
    [SerializeField] private LayerMask whatStopsMovement;
    [SerializeField] private bool isDebug = false;

    private GridBase levelGrid;


    void Awake() // 이 함수는 Start() 보다 먼저 실행된다. 자세한 사항은 #20221110 notion 개발일지 참고.
    {
        originPosition = new Vector3(-1.5f, -1.5f, 0f);
        levelGrid = new GridBase(x, y, cellSize, originPosition, isDebug);
        SetWall();
    }

    private void SetWall()
    {
        for (int x = 0; x < levelGrid.width; x++)
        {
            for (int y = 0; y < levelGrid.height; y++)
            {
                if (Physics2D.OverlapCircle(new Vector3(x - 1, y - 1, 0f), .2f, whatStopsMovement))
                {
                    //Debug.Log("-1 로 초기화");
                    levelGrid.SetValue(x, y, -1);
                }
            }
        }
    }

    public GridBase GetGrid()
    {
        return levelGrid;
    }

}
