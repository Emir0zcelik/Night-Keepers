using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    public const int sortingOrderDefault = 5000; // Code Monkey Utils --> Will be moving to another script like utils.cs...

    private int _width;
    private int _height;
    private int _cellsize;
    private T [,] _grid;
    private TextMesh[,] _debugText;

    public Grid(int width, int height, int cellsize)
    {
        _width = width;
        _height = height;
        _cellsize = cellsize;
        _grid = new T[_width, _height];
        _debugText = new TextMesh[_width, _height];

        for (int x = 0; x < _grid.GetLength(0); x++)
        {
            for (int z = 0; z < _grid.GetLength(1); z++)
            {
                _debugText[x,z] = CreateWorldText(_grid[x, z].ToString(), null, GridToWorldPosition(new Vector2Int(x, z)), 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GridToWorldPositionDrawLine(new Vector2Int(x, z)), GridToWorldPositionDrawLine(new Vector2Int(x, z + 1)), Color.white, 100f);
                Debug.DrawLine(GridToWorldPositionDrawLine(new Vector2Int(x, z)), GridToWorldPositionDrawLine(new Vector2Int(x + 1, z)), Color.white, 100f);
            }
        }
        Debug.DrawLine(GridToWorldPositionDrawLine(new Vector2Int(0, _height)), GridToWorldPositionDrawLine(new Vector2Int(_width, _height)) , Color.white, 100f);
        Debug.DrawLine(GridToWorldPositionDrawLine(new Vector2Int(_width, 0)), GridToWorldPositionDrawLine(new Vector2Int(_width, _height)), Color.white, 100f);
    }
    
    public void SetItem(Vector2Int gridPosition, T item)
    {
        _grid[gridPosition.x, gridPosition.y] = item;
        _debugText[gridPosition.x, gridPosition.y].text = _grid[gridPosition.x, gridPosition.y].ToString();
    }

    public void SetItem(Vector3 worldPosition, T item)
    {
        Vector2Int gridPosition = WorldToGridPosition(worldPosition);
        SetItem(gridPosition, item);
    }

    public T GetItem(Vector2Int gridPosition)
    {
        return _grid[gridPosition.x, gridPosition.y];
    }

    public T GetItem(Vector3 worldPosition)
    {
        Vector2Int gridPosition = WorldToGridPosition(worldPosition);
        T value = GetItem(gridPosition);

        return value;
    }
    
    public Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.y) * _cellsize + new Vector3(1, 0, 1) * (_cellsize * 0.5f);
    }
    
    private Vector3 GridToWorldPositionDrawLine(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.y) * _cellsize;
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / _cellsize);
        int z = Mathf.FloorToInt(worldPosition.z / _cellsize);

        return new Vector2Int(x, z);
    }

    public bool IsInDimensions(Vector2Int gridPosition)
    {
        return (gridPosition.x >= 0 && gridPosition.y >= 0 && gridPosition.x < _grid.GetLength(0) && gridPosition.y < _grid.GetLength(1));
    }
    
    
    // Code Monkey Utils --> Will be moving to another script like utils.cs...
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault) {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.rotation = Quaternion.Euler(90f, 0, 0);
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
    
}
