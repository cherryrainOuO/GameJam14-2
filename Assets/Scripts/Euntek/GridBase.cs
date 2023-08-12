using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridValue { VOID_VALUE, GROUND_VALUE }

public class GridBase
{
    /***** 필드 *****/
    ///////////////////////////////////////////////////


    public int width { get; private set; }
    public int height { get; private set; }
    private float cellSize; // 칸 사이즈
    private Vector3 originPosition; // 시작위치

    private PathNode[,] gridArray; // 실제 저장 그리드배열
    private TextMesh[,] debugTextArray; ///// 디버깅 출력용 텍스트매쉬


    private bool isDebug = false; //! 디버깅용
    ///////////////////////////////////////////////////


    /***** 생성자 *****/
    ///////////////////////////////////////////////////
    public GridBase(int width, int height, float cellSize, Vector3 originPosition, bool _isDebug = false)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.isDebug = _isDebug;

        gridArray = new PathNode[width, height];
        debugTextArray = new TextMesh[width, height];


        /* 그리드 배열 생성파트 */
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Debug.Log(x +", " + y);

                gridArray[x, y] = new PathNode(x, y, (int)GridValue.GROUND_VALUE);

                //// 디버깅용 배열 구분선
                if (isDebug)
                {
                    debugTextArray[x, y] = CreateWorldText(gridArray[x, y].stateValue.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f + new Vector3(-.3f, .3f), 40, Color.white, TextAnchor.MiddleCenter);

                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                }

                ////
            }
        }
        //// 디버깅용 배열 구분선
        if (isDebug)
        {
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        }
        ////
    }
    ///////////////////////////////////////////////////


    /***** 메서드 *****/
    ///////////////////////////////////////////////////
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    /** 마우스 포인트용 x, y 반환 **/
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height && gridArray[x, y].stateValue != (int)GridValue.VOID_VALUE)
        {
            gridArray[x, y].stateValue = value;

            ////
            if (isDebug)
            {
                debugTextArray[x, y].text = gridArray[x, y].stateValue.ToString();
                //debugTextArray[x, y].color = (value == MOVEABLE_VALUE) ? Color.cyan : Color.white;
            }
            ////
        }
    }
    /** 마우스 포인트용 오버로딩 **/
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y].stateValue;
        }
        else
        {
            return -1;
        }
    }
    /** 마우스 포인트용 오버로딩 **/
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    /** 그리드 초기화 (target 을 기준으로) **/
    // SetValue와는 다르게 3, -1 조건 없음.
    public void Clear(int target, int value)
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (GetValue(x, y) == target)
                {
                    gridArray[x, y].stateValue = value;
                    ////
                    if (isDebug)
                    {
                        debugTextArray[x, y].text = gridArray[x, y].stateValue.ToString();
                        //debugTextArray[x, y].color = (value == MOVEABLE_VALUE) ? Color.cyan : Color.white;
                    }
                    ////
                }
            }
        }
    }

    public PathNode GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(PathNode);
        }
    }
    /** 마우스 포인트용 오버로딩 **/
    public PathNode GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    public void SetLimitArea(int x, int y, Vector3[] limitAreas)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y].limitArea = limitAreas;
            gridArray[x, y].limitValue = limitAreas.Length;

            if (isDebug)
                CreateWorldText(gridArray[x, y].limitValue.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f + new Vector3(.3f, .3f), 40, Color.cyan, TextAnchor.MiddleCenter);
        }
    }
    ///////////////////////////////////////////////////


    /***** 이하의 내용은 디버깅용 텍스트매쉬 생성 함수 **건드리지 않는편이 좋음 *****/
    // 수정해도 되는 항목 : fontSize, localScale ... 
    ///////////////////////////////////////////////////
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

        textMesh.transform.localScale = new Vector3(.08f, .08f, .08f); // 텍스트매쉬 폰트사이즈 업 -> 스케일 다운 => (선명하게)
        return textMesh;
    }
    ///////////////////////////////////////////////////
}
