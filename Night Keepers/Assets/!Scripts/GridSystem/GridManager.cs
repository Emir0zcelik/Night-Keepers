using System;
using System.Collections;
using System.Collections.Generic;
using Bitgem.VFX.StylisedWater;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WayOfTile
{
    Empty, Up, Down, Left, Right
}

public class GridManager : Singleton<GridManager>
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] public int cellSize;

    [SerializeField] private int grassProbability;
    [SerializeField] private int rockProbability;
    [SerializeField] private int waterProbability;
    [SerializeField] private int woodProbability;
    [SerializeField] private int ironProbability;

    [SerializeField] private int grassPropagation;
    [SerializeField] private int rockPropagation;
    [SerializeField] private int waterPropagation;
    [SerializeField] private int woodPropagation;
    [SerializeField] private int ironPropagation;

    [SerializeField] private float grassNoise;
    [SerializeField] private float rockNoise;
    [SerializeField] private float waterNoise;
    [SerializeField] private float woodNoise;
    [SerializeField] private float ironNoise;
    [SerializeField] private List<GameObject> tilePrefabs;
    [SerializeField] private GameObject waterParent;
    [SerializeField] private GameObject waterDeepCube;

    public static event Action onWorldGenerationDone;
    public Grid<Tile> _grid;
    private const int batchInstantiateSize = 1000;
    private void Awake()
    {
        _grid = new Grid<Tile>(width, height, cellSize);
        InstantiateMap();
    }
    private void Start() {

        foreach (var item in FindObjectsOfType<WaterVolumeBase>())
        {
            WaterFoamChanger(item, _grid.WorldToGridPosition(item.transform.position));
        }
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector2Int gridPosition = new Vector2Int(x, z);
                InstantiateWaterDepthWall(gridPosition);
                InstantiateWallAllEdges(gridPosition);
            }
        }
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
                    GenerateMap(gridPosition.x, gridPosition.y);
                }
            }

            emptyTileCount = GetEmptyTileCount();
        }
        onWorldGenerationDone?.Invoke();
    }

    void AroundTheBaseTile(int x, int z, TileType tileType)
    {
        if (_grid.IsInDimensions(new Vector2Int(x, z)) && _grid._grid[x,z].tileType == TileType.Empty)
        {
            _grid._grid[x,z].tileType = tileType;
            InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), tileType);
        }
    }

    void TilePropagation (int x, int z, TileType tileType, int size, float noise)
    {
        for (int i = x - size; i <= x + size; i++)
        {
            for (int j = z - size; j <= z + size; j++)
            {
                if (Random.value < noise) 
                {
                    continue;
                }
                AroundTheBaseTile(i, j, tileType);
            }
        }
    }

    void GenerateMap(int x, int z)
    {
        TileType tileType = _grid._grid[x, z].tileType;
        
        switch (tileType)
        {
            case TileType.Grass:                
                InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                TilePropagation(x, z, tileType, Random.Range(1, grassPropagation), grassNoise);

                break;
            
            case TileType.Rock:
                
                InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                TilePropagation(x, z, tileType, Random.Range(1, rockPropagation), rockNoise);

                break;
            
            case TileType.Water:
                
                InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                TilePropagation(x, z, tileType, Random.Range(3, waterPropagation), waterNoise);

                break;

            case TileType.Wood:

                InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                TilePropagation(x, z, tileType, Random.Range(1, woodPropagation), woodNoise);

                break;

            case TileType.Iron:

                InstantiateTile(_grid.GridToWorldPosition(new Vector2Int(x, z)), _grid._grid[x, z].tileType);

                TilePropagation(x, z, tileType, Random.Range(1, ironPropagation), ironNoise);

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
                tilePrefab = Instantiate(waterParent, position, quaternion.identity);
                tilePrefab.transform.position = new Vector3(tilePrefab.transform.position.x - 4.5f, -11f, tilePrefab.transform.position.z - 4.5f);
                Instantiate(waterDeepCube, new Vector3(position.x, Random.Range(-15, -10) ,position.z), Quaternion.identity);
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

        if (randomNumber <= grassProbability)
        {
            tileType = TileType.Grass;
        }

        if (randomNumber > grassProbability && randomNumber <= rockProbability)
        {
            tileType = TileType.Rock;
        }

        if (randomNumber > rockProbability && randomNumber <= waterProbability)
        {
            tileType = TileType.Water;
        }

        if (randomNumber > waterProbability && randomNumber <= woodProbability)
        {
            tileType = TileType.Wood;
        }

        if (randomNumber > woodProbability && randomNumber <= ironProbability)
        {
            tileType = TileType.Iron;
        }

        return tileType;
    }

    public int GetMapSizeFromCenter()
    {
        return width * cellSize / 2;
    }

    private void InstantiateWallAllEdges(Vector2Int gridPosition)
    {
        if (_grid[gridPosition].tileType == TileType.Water) {return; }

        int tileTypeNumber = 0;

        if (_grid[gridPosition].tileType == TileType.Grass)
        {
            tileTypeNumber = 0;
        }
        if (_grid[gridPosition].tileType == TileType.Wood)
        {
            tileTypeNumber = 3;
        }
        if (_grid[gridPosition].tileType == TileType.Rock)
        {
            tileTypeNumber = 1;
        }

        if (gridPosition.x == 0)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x - 5f, -5f, _grid.GridToWorldPosition(gridPosition).z), 
                Quaternion.Euler(90f, 180f, -90));
        }

        if (gridPosition.x == width - 1)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x + 5f, -5f, _grid.GridToWorldPosition(gridPosition).z), 
                Quaternion.Euler(90f, 180f, 90));
        }

        if (gridPosition.y == 0)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x, -5f, _grid.GridToWorldPosition(gridPosition).z -5f), 
                Quaternion.Euler(90f, 90f, -90));
        }

        if (gridPosition.y == height - 1)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x, -5f, _grid.GridToWorldPosition(gridPosition).z +5f), 
                Quaternion.Euler(90f, 90f, 90));
        }


    }

    private void InstantiateWaterDepthWall(Vector2Int gridPosition)
    {
        if (_grid[gridPosition].tileType == TileType.Water) {return; }

        int tileTypeNumber = 0;

        if (_grid[gridPosition].tileType == TileType.Grass)
        {
            tileTypeNumber = 0;
        }
        if (_grid[gridPosition].tileType == TileType.Wood)
        {
            tileTypeNumber = 3;
        }
        if (_grid[gridPosition].tileType == TileType.Rock)
        {
            tileTypeNumber = 1;
        }


        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x - 1, gridPosition.y)) && _grid[gridPosition.x - 1, gridPosition.y].tileType == TileType.Water)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x - 5f, -5f, _grid.GridToWorldPosition(gridPosition).z), 
                Quaternion.Euler(90f, 180f, -90));
        }

        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x + 1, gridPosition.y)) && _grid[gridPosition.x + 1, gridPosition.y].tileType == TileType.Water)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x + 5f, -5f, _grid.GridToWorldPosition(gridPosition).z), 
                Quaternion.Euler(90f, 90f, 0));
        }

        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x, gridPosition.y - 1)) && _grid[gridPosition.x, gridPosition.y - 1].tileType == TileType.Water)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x, -5f, _grid.GridToWorldPosition(gridPosition).z - 5f),
                Quaternion.Euler(90f, 90f, -90));
        }
        
        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x, gridPosition.y + 1)) && _grid[gridPosition.x, gridPosition.y + 1].tileType == TileType.Water)
        {
            Instantiate(
                tilePrefabs[tileTypeNumber], 
                new Vector3(_grid.GridToWorldPosition(gridPosition).x, -5f, _grid.GridToWorldPosition(gridPosition).z + 5f),
                Quaternion.Euler(90f, 90f, 90));
        }
    }


    private void WaterFoamChanger(WaterVolumeBase waterVolumeBase, Vector2Int gridPosition)
    {
        waterVolumeBase.IncludeFoam = 0;
        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x - 1, gridPosition.y)) && _grid[gridPosition.x - 1, gridPosition.y].tileType != TileType.Water)
        {
            waterVolumeBase.IncludeFoam |= WaterVolumeBase.TileFace.NegX;
        }
        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x + 1, gridPosition.y)) && _grid[gridPosition.x + 1, gridPosition.y].tileType != TileType.Water)
        {
            waterVolumeBase.IncludeFoam |= WaterVolumeBase.TileFace.PosX;
        }
        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x, gridPosition.y - 1)) && _grid[gridPosition.x, gridPosition.y - 1].tileType != TileType.Water)
        {
            waterVolumeBase.IncludeFoam |= WaterVolumeBase.TileFace.NegZ;
        }
        if (_grid.IsInDimensions(new Vector2Int(gridPosition.x, gridPosition.y + 1)) && _grid[gridPosition.x, gridPosition.y + 1].tileType != TileType.Water)
        {
            waterVolumeBase.IncludeFoam |= WaterVolumeBase.TileFace.PosZ;
        }
    }
}
