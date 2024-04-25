using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private List<Building> building;
    [SerializeField] private List<GameObject> buildingPreviews;
    [SerializeField] private Material validPreviewMaterial;
    [SerializeField] private Material invalidPreviewMaterial;
    private BuildingData.BuildingType buildingType;
    private bool isRotated = false;

    private int buildingNumber;
    bool isPreviewRotated = false;

    bool isPlaced = false;

    private void Start() {
        foreach (var building in buildingPreviews)
        {
            building.transform.localScale = new Vector3(building.transform.localScale.x * _gridManager.cellSize / 10, building.transform.localScale.y * _gridManager.cellSize / 10, building.transform.localScale.z * _gridManager.cellSize / 10);
            building.SetActive(false);
        }
    }
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        SelectBuilding();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            Vector2Int gridPosition = _gridManager._grid.WorldToGridPosition(raycastHit.point);

            Debug.Log("Grid Pos: " + gridPosition);

            PreviewBuilding(gridPosition);

            PlaceBuilding(gridPosition);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isRotated = true;
            isPreviewRotated = true;
        }
    }

    private void SelectBuilding()
    {
        switch (buildingType)
        {
            case BuildingData.BuildingType.StoneMine:
                isPlaced = false;
                buildingNumber = 0;
                BuildingPreviewsActivate(buildingNumber);
                break;

            case BuildingData.BuildingType.IronMine:
                isPlaced = false;
                buildingNumber = 1;
                BuildingPreviewsActivate(buildingNumber);
                break;

            case BuildingData.BuildingType.Lumberjack:
                isPlaced = false;
                buildingNumber = 2;
                BuildingPreviewsActivate(buildingNumber);
                
                break;

            case BuildingData.BuildingType.TownHall:
                isPlaced = false;
                buildingNumber = 3;
                BuildingPreviewsActivate(buildingNumber);
                break;

            case BuildingData.BuildingType.Test:
                isPlaced = false;
                buildingNumber = 4;
                BuildingPreviewsActivate(buildingNumber);
                break;
        }

    }

    private void BuildingPreviewsActivate(int active)
    {
        for (int i = 0; i < buildingPreviews.Count; i++)
        {
            if (i == active)
            {
                buildingPreviews[i].SetActive(true);
            }
            else
            {
                buildingPreviews[i].SetActive(false);
            }

        }
    }

    private void PreviewBuilding(Vector2Int gridPosition)
    {
        if(!isPlaced)
        {
            if (TryBuild(building[buildingNumber], building[buildingNumber].buildingData.GetGridPositionList(gridPosition, building[buildingNumber].direction)))
            {
                buildingPreviews[buildingNumber].GetComponent<MeshRenderer>().material = validPreviewMaterial;
            }
            else
            {
                buildingPreviews[buildingNumber].GetComponent<MeshRenderer>().material = invalidPreviewMaterial;                
            }
            buildingPreviews[buildingNumber].transform.position = _gridManager._grid.GridToWorldPosition(gridPosition);
            if (isPreviewRotated)
            {
                buildingPreviews[buildingNumber].GetComponent<Building>().direction = BuildingData.GetNextDir(buildingPreviews[buildingNumber].GetComponent<Building>().direction);
                buildingPreviews[buildingNumber].transform.rotation = Quaternion.Euler(0, buildingPreviews[buildingNumber].GetComponent<Building>().buildingData.GetRotationAngle(buildingPreviews[buildingNumber].GetComponent<Building>().direction), 0);                
                isPreviewRotated = false;
            }
        }
        
    }

    private void PlaceBuilding(Vector2Int gridPosition)
    {
        if (isRotated)
        {
            building[buildingNumber].direction = BuildingData.GetNextDir(building[buildingNumber].direction);
            isRotated = false;
        }

        if (Input.GetMouseButton(0))
        {
            List<Vector2Int> gridPositionList = building[buildingNumber].buildingData.GetGridPositionList(gridPosition, building[buildingNumber].direction);
            building[buildingNumber].transform.position = _gridManager._grid.GridToWorldPosition(gridPosition);

            if (TryBuild(building[buildingNumber], gridPositionList))
            {                
                Vector2Int rotationOffset = building[buildingNumber].buildingData.GetRotationOffset(building[buildingNumber].direction);
                Vector3 instantiatedBuildingWorldPosition = _gridManager._grid.GridToWorldPosition(gridPosition) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _gridManager.cellSize;
                Building instantiatedBuilding = Instantiate(
                        building[buildingNumber],
                        instantiatedBuildingWorldPosition,
                        Quaternion.Euler(0, building[buildingNumber].buildingData.GetRotationAngle(buildingPreviews[buildingNumber].GetComponent<Building>().direction), 0));
        
                instantiatedBuilding.transform.localScale = new Vector3(instantiatedBuilding.transform.localScale.x * _gridManager.cellSize / 10, instantiatedBuilding.transform.localScale.y * _gridManager.cellSize / 10, instantiatedBuilding.transform.localScale.z * _gridManager.cellSize / 10);
               
                foreach (Vector2Int position in gridPositionList)
                {
                    Tile tile = new Tile()
                    {
                        building = instantiatedBuilding
                    };
                    _gridManager._grid[position] = tile;
                }
            }
        }
        
    }

    public void SetBuildingType(BuildingData.BuildingType buildingType)
    {
        this.buildingType = buildingType;
    }

    private bool TryBuild(Building building, List<Vector2Int> gridPositionList)
    {
        foreach (Vector2Int position in gridPositionList)
        {
            if (_gridManager._grid[position].building != null)
            {
                return false;
            }
            if (building.buildingData.placableTileTypes[0] != _gridManager._grid[position].tileType)
            {
                return false;
            }
        }

        isPlaced = true;
        
        return true;
    }
}
