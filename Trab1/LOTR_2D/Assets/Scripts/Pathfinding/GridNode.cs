using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public int x;
    public int y;

    public int f;
    public int g; 
    public int h; 
    public char type;

    public GridNode parent;

    public GameObject mapTile;

    public GridNode(int x, int y, char type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }

    public int GetFCost()
    {
        f = g + h;
        return f;
    }
}
