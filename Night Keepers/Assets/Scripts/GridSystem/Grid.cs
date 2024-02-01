using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid <T>
{
    private int _cellSize;
    private T[,] _grid;

    public Grid(T[,] grid, int cellSize)
    {
        _grid = grid;
        _cellSize = cellSize;
    }
    
    public void SetItem(Vector2Int gridPosition, T item)
    {
        _grid[gridPosition.x, gridPosition.y] = item;
    }

    public T GetItem(Vector2Int gridPosition)
    {
        return _grid[gridPosition.x, gridPosition.y];
    }

    public Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        return (new Vector3(gridPosition.x, gridPosition.y) * _cellSize) + (Vector3)(Vector2.one * _cellSize / 2);
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        int z = Mathf.FloorToInt(worldPosition.z / _cellSize);

        return new Vector2Int(x, z);
    }

    public Vector3 GetCenterOfGrid()
    {
        return new Vector3(_grid.GetLength(0) * _cellSize * .5f, 0, _grid.GetLength(1) * _cellSize * .5f);
    }

    public bool IsInDimensions(int x, int y)
    {
        return x >= 0 && x < _grid.GetLength(0) && y >= 0 && y < _grid.GetLength(1);
    }
    
}
