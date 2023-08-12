using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int stateValue;

    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public int limitValue;
    public Vector3[] limitArea;

    public PathNode(int x, int y, int value){     
        this.x = x;
        this.y = y;
        this.stateValue = value;
        this.limitValue = 0;
    }

    public void CalculateFcost(){
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
