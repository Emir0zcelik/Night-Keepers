using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int cellSize;

    [SerializeField] private int grassProbability;
    [SerializeField] private int rockProbability;
    [SerializeField] private int waterProbability;
    [SerializeField] private int woodProbability;
    [SerializeField] private int ironProbability;

    [SerializeField] private List<Building> building;
    [SerializeField] private List<GameObject> tilePrefabs;

    private bool isGenerated = false;

    private External _external;
    private Grid<Tile> _grid;
    private TextMesh[,] _debugText;
    public const int sortingOrderDefault = 5000;
    private const int batchInstantiateSize = 1000;
    private void Awake()
    {
        _grid = new Grid<Tile>(width, height, cellSize);
        DebugLines();
        InstantiateMap();
    }

    private void Update()
    {  

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                Vector2Int gridPosition = _grid.WorldToGridPosition(raycastHit.point);
                Debug.Log(_grid._grid[gridPosition.x, gridPosition.y].tileType);
            }
        }
        
        // !!!! Rotate action for building 
        
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     building[4].direction = BuildingData.GetNextDir(building[4].direction);
        // }
    }

    void InstantiateMap()
    {
        int emptyTileCount = GetEmptyTileCount();

        while (emptyTileCount > 0)
        {
            int batchSize = Mathf.Min(emptyTileCount, batchInstantiateSize);

            for (int i = 0; i < batchSize; i++)
            {
                Vector2Int gridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));

                if (IsTileTypeEmpty(gridPosition.x, gridPosition.y))
                {
                    _grid._grid[gridPosition.x, gridPosition.y].tileType = GetRandomTileType();

                    _debugText[gridPosition.x, gridPosition.y].text = _grid._grid[gridPosition.x, gridPosition.y].tileType.ToString();
                    
                    GenerateMap(gridPosition.x, gridPosition.y);
                }
            }

            emptyTileCount = GetEmptyTileCount();
        }
    }


    void GenerateMap(int x, int z)
    {
        TileType tileType = _grid._grid[x, z].tileType;
        GameObject middleTile;
        GameObject tile;
        
        switch (tileType)
        {
            case TileType.Grass:
                
                _debugText[x, z].color = Color.green;
                middleTile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                if (_grid.IsInDimensions(new Vector2Int(x + 1, z)) && _grid._grid[x + 1, z].tileType == TileType.Empty)
                {
                    _grid._grid[x + 1, z].tileType = TileType.Grass;
                    _debugText[x + 1, z].text = _grid._grid[x + 1, z].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z)), _grid._grid[x, z].tileType);
                    _debugText[x + 1, z].color = Color.green;
                }
                
                if (_grid.IsInDimensions(new Vector2Int(x, z + 1)) && _grid._grid[x, z + 1].tileType == TileType.Empty)
                {
                    _grid._grid[x, z + 1].tileType = TileType.Grass;
                    _debugText[x, z + 1].text = _grid._grid[x, z + 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z + 1)), _grid._grid[x, z].tileType);
                    _debugText[x, z + 1].color = Color.green;
                }

                if (_grid.IsInDimensions(new Vector2Int(x + 1, z + 1)) && _grid._grid[x + 1, z + 1].tileType == TileType.Empty)
                {
                    _grid._grid[x + 1, z + 1].tileType = TileType.Grass;
                    _debugText[x + 1, z + 1].text = _grid._grid[x + 1, z + 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z + 1)), _grid._grid[x, z].tileType);
                    _debugText[x + 1, z + 1].color = Color.green;
                }

                if (_grid.IsInDimensions(new Vector2Int(x - 1, z)) && _grid._grid[x - 1, z].tileType == TileType.Empty)
                {
                    _grid._grid[x - 1, z].tileType = TileType.Grass;
                    _debugText[x - 1, z].text = _grid._grid[x - 1, z].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z)), _grid._grid[x, z].tileType);
                    _debugText[x - 1, z].color = Color.green;
                }
                
                if (_grid.IsInDimensions(new Vector2Int(x, z - 1)) && _grid._grid[x, z - 1].tileType == TileType.Empty)
                {
                    _grid._grid[x, z - 1].tileType = TileType.Grass;
                    _debugText[x, z - 1].text = _grid._grid[x, z - 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z - 1)), _grid._grid[x, z].tileType);
                    _debugText[x, z - 1].color = Color.green;
                }

                if (_grid.IsInDimensions(new Vector2Int(x - 1, z - 1)) && _grid._grid[x - 1, z - 1].tileType == TileType.Empty)
                {
                    _grid._grid[x - 1, z - 1].tileType = TileType.Grass;
                    _debugText[x - 1, z - 1].text = _grid._grid[x - 1, z - 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z - 1)), _grid._grid[x, z].tileType);
                    _debugText[x - 1, z - 1].color = Color.green;
                }


                break;
            
            case TileType.Rock:
                
                _debugText[x, z].color = Color.yellow;
                middleTile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                if (_grid.IsInDimensions(new Vector2Int(x + 1, z)) && _grid._grid[x + 1, z].tileType == TileType.Empty)
                {
                    _grid._grid[x + 1, z].tileType = TileType.Rock;
                    _debugText[x + 1, z].text = _grid._grid[x + 1, z].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z)), _grid._grid[x, z].tileType);
                    _debugText[x + 1, z].color = Color.yellow;
                }
                
                if (_grid.IsInDimensions(new Vector2Int(x, z + 1)) && _grid._grid[x, z + 1].tileType == TileType.Empty)
                {
                    _grid._grid[x, z + 1].tileType = TileType.Rock;
                    _debugText[x, z + 1].text = _grid._grid[x, z + 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z + 1)), _grid._grid[x, z].tileType);
                    _debugText[x, z + 1].color = Color.yellow;
                }

                if (_grid.IsInDimensions(new Vector2Int(x + 1, z + 1)) && _grid._grid[x + 1, z + 1].tileType == TileType.Empty)
                {
                    _grid._grid[x + 1, z + 1].tileType = TileType.Rock;
                    _debugText[x + 1, z + 1].text = _grid._grid[x + 1, z + 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z + 1)), _grid._grid[x, z].tileType);
                    _debugText[x + 1, z + 1].color = Color.yellow;
                }

                if (_grid.IsInDimensions(new Vector2Int(x - 1, z)) && _grid._grid[x - 1, z].tileType == TileType.Empty)
                {
                    _grid._grid[x - 1, z].tileType = TileType.Rock;
                    _debugText[x - 1, z].text = _grid._grid[x - 1, z].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z)), _grid._grid[x, z].tileType);
                    _debugText[x - 1, z].color = Color.yellow;
                }
                
                if (_grid.IsInDimensions(new Vector2Int(x, z - 1)) && _grid._grid[x, z - 1].tileType == TileType.Empty)
                {
                    _grid._grid[x, z - 1].tileType = TileType.Rock;
                    _debugText[x, z - 1].text = _grid._grid[x, z - 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z - 1)), _grid._grid[x, z].tileType);
                    _debugText[x, z - 1].color = Color.yellow;
                }

                if (_grid.IsInDimensions(new Vector2Int(x - 1, z - 1)) && _grid._grid[x - 1, z - 1].tileType == TileType.Empty)
                {
                    _grid._grid[x - 1, z - 1].tileType = TileType.Rock;
                    _debugText[x - 1, z - 1].text = _grid._grid[x - 1, z - 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z - 1)), _grid._grid[x, z].tileType);
                    _debugText[x - 1, z - 1].color = Color.yellow;
                }
                
                break;
            
            case TileType.Water:
                
                _debugText[x, z].color = Color.blue;
                middleTile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                if (_grid.IsInDimensions(new Vector2Int(x + 1, z)) && _grid._grid[x + 1, z].tileType == TileType.Empty)
                {
                    _grid._grid[x + 1, z].tileType = TileType.Water;
                    _debugText[x + 1, z].text = _grid._grid[x + 1, z].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z)), _grid._grid[x, z].tileType);
                    _debugText[x + 1, z].color = Color.blue;
                }
                
                if (_grid.IsInDimensions(new Vector2Int(x, z + 1)) && _grid._grid[x, z + 1].tileType == TileType.Empty)
                {
                    _grid._grid[x, z + 1].tileType = TileType.Water;
                    _debugText[x, z + 1].text = _grid._grid[x, z + 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z + 1)), _grid._grid[x, z].tileType);
                    _debugText[x, z + 1].color = Color.blue;
                }

                if (_grid.IsInDimensions(new Vector2Int(x + 1, z + 1)) && _grid._grid[x + 1, z + 1].tileType == TileType.Empty)
                {
                    _grid._grid[x + 1, z + 1].tileType = TileType.Water;
                    _debugText[x + 1, z + 1].text = _grid._grid[x + 1, z + 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z + 1)), _grid._grid[x, z].tileType);
                    _debugText[x + 1, z + 1].color = Color.blue;
                }

                if (_grid.IsInDimensions(new Vector2Int(x - 1, z)) && _grid._grid[x - 1, z].tileType == TileType.Empty)
                {
                    _grid._grid[x - 1, z].tileType = TileType.Water;
                    _debugText[x - 1, z].text = _grid._grid[x - 1, z].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z)), _grid._grid[x, z].tileType);
                    _debugText[x - 1, z].color = Color.blue;
                }
                
                if (_grid.IsInDimensions(new Vector2Int(x, z - 1)) && _grid._grid[x, z - 1].tileType == TileType.Empty)
                {
                    _grid._grid[x, z - 1].tileType = TileType.Water;
                    _debugText[x, z - 1].text = _grid._grid[x, z - 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z - 1)), _grid._grid[x, z].tileType);
                    _debugText[x, z - 1].color = Color.blue;
                }

                if (_grid.IsInDimensions(new Vector2Int(x - 1, z - 1)) && _grid._grid[x - 1, z - 1].tileType == TileType.Empty)
                {
                    _grid._grid[x - 1, z - 1].tileType = TileType.Water;
                    _debugText[x - 1, z - 1].text = _grid._grid[x - 1, z - 1].tileType.ToString();
                    tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z - 1)), _grid._grid[x, z].tileType);
                    _debugText[x - 1, z - 1].color = Color.blue;
                }

                break;

                case TileType.Wood:

                    _debugText[x, z].color = Color.red;
                    middleTile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                    if (_grid.IsInDimensions(new Vector2Int(x + 1, z)) && _grid._grid[x + 1, z].tileType == TileType.Empty)
                    {
                        _grid._grid[x + 1, z].tileType = TileType.Wood;
                        _debugText[x + 1, z].text = _grid._grid[x + 1, z].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z)), _grid._grid[x, z].tileType);
                        _debugText[x + 1, z].color = Color.red;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x, z + 1)) && _grid._grid[x, z + 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x, z + 1].tileType = TileType.Wood;
                        _debugText[x, z + 1].text = _grid._grid[x, z + 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z + 1)), _grid._grid[x, z].tileType);
                        _debugText[x, z + 1].color = Color.red;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x + 1, z + 1)) && _grid._grid[x + 1, z + 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x + 1, z + 1].tileType = TileType.Wood;
                        _debugText[x + 1, z + 1].text = _grid._grid[x + 1, z + 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z + 1)), _grid._grid[x, z].tileType);
                        _debugText[x + 1, z + 1].color = Color.red;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x - 1, z)) && _grid._grid[x - 1, z].tileType == TileType.Empty)
                    {
                        _grid._grid[x - 1, z].tileType = TileType.Wood;
                        _debugText[x - 1, z].text = _grid._grid[x - 1, z].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z)), _grid._grid[x, z].tileType);
                        _debugText[x - 1, z].color = Color.red;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x, z - 1)) && _grid._grid[x, z - 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x, z - 1].tileType = TileType.Wood;
                        _debugText[x, z - 1].text = _grid._grid[x, z - 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z - 1)), _grid._grid[x, z].tileType);
                        _debugText[x, z - 1].color = Color.red;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x - 1, z - 1)) && _grid._grid[x - 1, z - 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x - 1, z - 1].tileType = TileType.Wood;
                        _debugText[x - 1, z - 1].text = _grid._grid[x - 1, z - 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z - 1)), _grid._grid[x, z].tileType);
                        _debugText[x - 1, z - 1].color = Color.red;
                    }

                    break;

                case TileType.Iron:

                    _debugText[x, z].color = Color.magenta;
                    middleTile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                    if (_grid.IsInDimensions(new Vector2Int(x + 1, z)) && _grid._grid[x + 1, z].tileType == TileType.Empty)
                    {
                        _grid._grid[x + 1, z].tileType = TileType.Iron;
                        _debugText[x + 1, z].text = _grid._grid[x + 1, z].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z)), _grid._grid[x, z].tileType);
                        _debugText[x + 1, z].color = Color.magenta;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x, z + 1)) && _grid._grid[x, z + 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x, z + 1].tileType = TileType.Iron;
                        _debugText[x, z + 1].text = _grid._grid[x, z + 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z + 1)), _grid._grid[x, z].tileType);
                        _debugText[x, z + 1].color = Color.magenta;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x + 1, z + 1)) && _grid._grid[x + 1, z + 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x + 1, z + 1].tileType = TileType.Iron;
                        _debugText[x + 1, z + 1].text = _grid._grid[x + 1, z + 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x + 1, z + 1)), _grid._grid[x, z].tileType);
                        _debugText[x + 1, z + 1].color = Color.magenta;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x - 1, z)) && _grid._grid[x - 1, z].tileType == TileType.Empty)
                    {
                        _grid._grid[x - 1, z].tileType = TileType.Iron;
                        _debugText[x - 1, z].text = _grid._grid[x - 1, z].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z)), _grid._grid[x, z].tileType);
                        _debugText[x - 1, z].color = Color.magenta;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x, z - 1)) && _grid._grid[x, z - 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x, z - 1].tileType = TileType.Iron;
                        _debugText[x, z - 1].text = _grid._grid[x, z - 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z - 1)), _grid._grid[x, z].tileType);
                        _debugText[x, z - 1].color = Color.magenta;
                    }

                    if (_grid.IsInDimensions(new Vector2Int(x - 1, z - 1)) && _grid._grid[x - 1, z - 1].tileType == TileType.Empty)
                    {
                        _grid._grid[x - 1, z - 1].tileType = TileType.Iron;
                        _debugText[x - 1, z - 1].text = _grid._grid[x - 1, z - 1].tileType.ToString();
                        tile = InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x - 1, z - 1)), _grid._grid[x, z].tileType);
                        _debugText[x - 1, z - 1].color = Color.magenta;
                    }

                    break;
        }
        
    }

    GameObject InstantiateTile(Vector3 position, TileType tileType)
    {
        GameObject tilePrefab = null;
        switch (tileType)
        {
            case TileType.Grass:
                tilePrefab = Instantiate(tilePrefabs[0], position, Quaternion.identity);
                break;
            
            case TileType.Rock:
                tilePrefab = Instantiate(tilePrefabs[1], position, quaternion.identity);
                break;  
            
            case TileType.Water:
                tilePrefab = Instantiate(tilePrefabs[2], position, quaternion.identity);
                break;
            
            case TileType.Wood:
                tilePrefab = Instantiate(tilePrefabs[3], position, quaternion.identity);
                break;
            
            case TileType.Iron:
                tilePrefab = Instantiate(tilePrefabs[4], position, quaternion.identity);
                break;
        }

        return tilePrefab;
    }

    int GetEmptyTileCount()
    {
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if(_grid._grid[x,z].tileType == TileType.Empty)
                {
                    count++;
                }
            }
        }

        return count;
    }

    bool IsTileTypeEmpty(int x, int z)
    {
        return _grid._grid[x,z].tileType == TileType.Empty ? true : false;
    }

    TileType GetRandomTileType()
    {
        TileType tileType = TileType.Empty;
        int randomNumber = Random.Range(0, 100);

        if (randomNumber <= 75)
        {
            tileType = TileType.Grass;
        }

        if (randomNumber > 75 && randomNumber <= 85)
        {
            tileType = TileType.Rock;
        }

        if (randomNumber > 85 && randomNumber <= 95)
        {
            tileType = TileType.Water;
        }

        if (randomNumber > 95 && randomNumber <= 98)
        {
            tileType = TileType.Wood;
        }

        if (randomNumber > 98 && randomNumber <= 100)
        {
            tileType = TileType.Iron;
        }

        return tileType;
    }
    

    private void DebugLines()
    {
        _debugText = new TextMesh[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                _debugText[x,z] = CreateWorldText(_grid._grid[x,z].tileType.ToString(), null, _grid.GridToWorldPosition(new Vector2Int(x, z)), 20, Color.white, TextAnchor.MiddleCenter);
                _debugText[x, z].color = Color.white;
                Debug.DrawLine(_grid.GridToWorldPositionDrawLine(new Vector2Int(x, z)), _grid.GridToWorldPositionDrawLine(new Vector2Int(x, z + 1)), Color.white, 100f);
                Debug.DrawLine(_grid.GridToWorldPositionDrawLine(new Vector2Int(x, z)), _grid.GridToWorldPositionDrawLine(new Vector2Int(x + 1, z)), Color.white, 100f);
            }
        }
        Debug.DrawLine(_grid.GridToWorldPositionDrawLine(new Vector2Int(0, height)), _grid.GridToWorldPositionDrawLine(new Vector2Int(width, height)) , Color.white, 100f);
        Debug.DrawLine(_grid.GridToWorldPositionDrawLine(new Vector2Int(width, 0)), _grid.GridToWorldPositionDrawLine(new Vector2Int(width, height)), Color.white, 100f);
    }
    
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
