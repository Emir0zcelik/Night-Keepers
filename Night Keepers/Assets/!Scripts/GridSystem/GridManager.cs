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
    [SerializeField] private List<Building> building;
    
    private Grid<Tile> _grid;
    private void Awake()
    {
        _grid = new Grid<Tile>(width, height, cellSize);
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

                List<Vector2Int> gridPositionList = building[4].buildingData.GetGridPositionList(gridPosition, building[4].direction);

                bool canBuild = true;

                foreach (Vector2Int position in gridPositionList)
                {
                    if (_grid[position].building != null)
                    {
                        canBuild = false;
                        break;
                    }
                }
                
                if (canBuild)
                {
                    Vector2Int rotationOffset = building[4].buildingData.GetRotationOffset(building[4].direction);
                    Vector3 instantiatedBuildingWorldPosition = _grid.GridToWorldPosition(gridPosition) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * cellSize;
                    Building instantiatedBuilding = 
                        Instantiate(
                            building[4],
                            instantiatedBuildingWorldPosition, 
                            Quaternion.Euler(0, building[4].buildingData.GetRotationAngle(building[4].direction), 0));
                    foreach (Vector2Int position in gridPositionList)
                    {
                        Tile tile = new Tile()
                        {
                            building = instantiatedBuilding
                        };
                    
                        _grid[position] = tile;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            building[4].direction = BuildingData.GetNextDir(building[4].direction);
        }
    }
}
