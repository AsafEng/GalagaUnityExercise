using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellModel
{
    public int X;
    public int Y;
    public Vector3 Position;
    public GameObject GridCellObject;
    public GridObjectType ObjectType;

    public GridCellModel(int gridX, int gridY, Vector3 realPos, GameObject newGridObject, GridObjectType type)
    {
        X = gridX;
        Y = gridY;
        Position = realPos;
        GridCellObject = newGridObject;
        ObjectType = type;
    }
}
