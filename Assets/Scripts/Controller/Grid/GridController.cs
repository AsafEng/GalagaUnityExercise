using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : Controller
{
    //Generated Grid Cells
    private GridCellModel[,] _generatedGrid;

    //Grid cell prefab
    [SerializeField]
    private GameObject _gridPrefab;

    [SerializeField]
    private GridModel _gridModel;

    [SerializeField]
    private Transform _gridContainer;

    private Vector2 _nextCell;

    //Getters of model's data
    #region
    public int GetGridSizeX()
    {
        return _gridModel.GridHorizontalSize;
    }

    public int GetGridSizeY()
    {
        return _gridModel.GridVerticalSize;
    }

    public float GetCellSizeX()
    {
        return _gridModel.GridHorizontalDiff;
    }

    public float GetCellSizeY()
    {
        return _gridModel.GridVerticalDiff;
    }
    #endregion

    private void Start()
    {
        //Init grid, according to the data model
        CreateGrid();
    }

    //Init grid cells
    private void CreateGrid()
    {
        _generatedGrid = new GridCellModel[GetGridSizeX(), GetGridSizeY()];
        for (int i = 0; i < GetGridSizeX(); i++)
        {
            for (int j = 0; j < GetGridSizeY(); j++)
            {
                GameObject newGridObject = Instantiate(_gridPrefab);
                _generatedGrid[i, j] = new GridCellModel(i, j, TransformGridToVectorPosition(i, j), newGridObject, GridObjectType.Empty);
                newGridObject.transform.parent = _gridContainer;
                newGridObject.transform.localPosition = TransformGridToVectorPosition(i, j);
                newGridObject.name = "GridCellObject " + i + "," + j;
            }
        }
    }

    //Helper function for getting real positions
    public Vector3 TransformGridToVectorPosition(int x, int y, bool isRelative = false)
    {
        float pivotTopLeftX = (y % 2 == 0) ? 0 : GetCellSizeX() / 2.0f;
        float positionX = pivotTopLeftX + x * GetCellSizeX() + GetCellSizeX() / 2.0f;
        float positionY = -y * GetCellSizeY() - GetCellSizeY() / 2.0f;
        float relativeX = isRelative? _gridContainer.transform.position.x:0;
        float relativeY = isRelative? _gridContainer.transform.position.y:0;

        return new Vector3(positionX + relativeX, positionY + relativeY, 0);
    }

    //Get the next empty cell
    public Vector3 GetNextEmptyCell()
    {
        for (int i = 0; i < GetGridSizeY(); i++)
        {
            for (int j = 0; j < GetGridSizeX(); j++)
            {
                if (_generatedGrid[i, j].ObjectType == GridObjectType.Empty)
                {
                    _generatedGrid[i, j].ObjectType = GridObjectType.Filled;
                    _nextCell = new Vector2(j, i);
                    return TransformGridToVectorPosition(j, i, true);
                }
            }
        }

        return TransformGridToVectorPosition(0, 0);
    }

    public Vector2 GetNextEmptyCellByGrid()
    {
        return _nextCell;
    }

    public override void OnNotification(string p_event_path, Object p_target, params object[] p_data)
    {
        switch (p_event_path)
        {
            case ApplicationEvents.EnemyKilled:
                //If an enemy is killed, empty its grid cell
                Vector2 gridPosition = (Vector2)p_data[0];
                _generatedGrid[(int)gridPosition.x, (int)gridPosition.y].ObjectType = GridObjectType.Empty;
                break;
            default:
                break;
        }
    }
}
