using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private List<Building> building;
    private Grid<Tile> _grid;
    private void Awake()
    {
        _grid = new Grid<Tile>(10, 10, 10);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                int randomNumber = Random.Range(0, building.Count);
                Vector2Int gridPosition = _grid.WorldToGridPosition(raycastHit.point);
                
                if (_grid[gridPosition].building == null)
                {
                    Building instantiatedBuilding = Instantiate(building[randomNumber], _grid.GridToWorldPosition(gridPosition), quaternion.identity);
                    Tile tile = new Tile()
                    {
                        building = instantiatedBuilding
                    };
                    
                    _grid[gridPosition] = tile;
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                Vector2Int gridPosition = _grid.WorldToGridPosition(raycastHit.point);
            }
        }
    }

    private bool IsFull(Vector3 worldPosition)
    {
        Vector2Int gridPosition = _grid.WorldToGridPosition(worldPosition);
        return IsFull(gridPosition);
    }
    
    private bool IsFull(Vector2Int gridPosition)
    {
        if (_grid._grid[gridPosition.x, gridPosition.y].building != null)
        {
            return true;
        }
        return false;
    }
}
