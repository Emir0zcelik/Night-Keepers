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
    private BuildingData.BuildingType buildingType;
    private bool canBuild = false;

    private bool isRotated = false;

    private int buildingNumber;

    bool isPreview = false;
    bool isPreviewRotated = false;

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

        PreviewBuilding();

        if (Input.GetMouseButton(0) && canBuild)
        {
            PlaceBuilding(buildingNumber);
            canBuild = false;
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
                buildingNumber = 0;
                buildingPreviews[0].SetActive(true);
                buildingPreviews[1].SetActive(false);
                buildingPreviews[2].SetActive(false);
                buildingPreviews[3].SetActive(false);
                buildingPreviews[4].SetActive(false);
                break;

            case BuildingData.BuildingType.IronMine:
                buildingNumber = 1;
                buildingPreviews[1].SetActive(true);
                buildingPreviews[0].SetActive(false);
                buildingPreviews[2].SetActive(false);
                buildingPreviews[3].SetActive(false);
                buildingPreviews[4].SetActive(false);
                break;

            case BuildingData.BuildingType.Lumberjack:
                buildingNumber = 2;
                buildingPreviews[2].SetActive(true);
                buildingPreviews[0].SetActive(false);
                buildingPreviews[1].SetActive(false);
                buildingPreviews[3].SetActive(false);
                buildingPreviews[4].SetActive(false);
                
                break;

            case BuildingData.BuildingType.TownHall:
                buildingNumber = 3;
                buildingPreviews[3].SetActive(true);
                buildingPreviews[0].SetActive(false);
                buildingPreviews[1].SetActive(false);
                buildingPreviews[2].SetActive(false);
                buildingPreviews[4].SetActive(false);
                break;

            case BuildingData.BuildingType.Test:
                buildingNumber = 4;
                buildingPreviews[4].SetActive(true);
                buildingPreviews[0].SetActive(false);
                buildingPreviews[1].SetActive(false);
                buildingPreviews[2].SetActive(false);
                buildingPreviews[3].SetActive(false);
                break;
        }
    }

    private void PreviewBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            Vector2Int gridPosition = _gridManager._grid.WorldToGridPosition(raycastHit.point);
            
            buildingPreviews[buildingNumber].transform.position = _gridManager._grid.GridToWorldPosition(gridPosition);
            if (isPreviewRotated)
            {
                buildingPreviews[buildingNumber].GetComponent<Building>().direction = BuildingData.GetNextDir(buildingPreviews[buildingNumber].GetComponent<Building>().direction);
                buildingPreviews[buildingNumber].transform.rotation = Quaternion.Euler(0, buildingPreviews[buildingNumber].GetComponent<Building>().buildingData.GetRotationAngle(buildingPreviews[buildingNumber].GetComponent<Building>().direction), 0);                
                isPreviewRotated = false;
            }
        }

    }

    private void PlaceBuilding(int count)
    {

        if (isRotated)
        {
            building[count].direction = BuildingData.GetNextDir(building[count].direction);
            isRotated = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            Vector2Int gridPosition = _gridManager._grid.WorldToGridPosition(raycastHit.point);
            List<Vector2Int> gridPositionList = building[count].buildingData.GetGridPositionList(gridPosition, building[count].direction);

            building[count].transform.position = _gridManager._grid.GridToWorldPosition(gridPosition);

            bool canBuild = true;

            foreach (Vector2Int position in gridPositionList)
            {
                if (_gridManager._grid[position].building != null)
                {
                    canBuild = false;
                    break;
                }

                if (building[count].buildingData.placableTileTypes[0] != _gridManager._grid[position].tileType)
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {                
                Vector2Int rotationOffset = building[count].buildingData.GetRotationOffset(building[count].direction);
                Vector3 instantiatedBuildingWorldPosition = _gridManager._grid.GridToWorldPosition(gridPosition) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _gridManager.cellSize;
                Building instantiatedBuilding = Instantiate(
                        building[count],
                        instantiatedBuildingWorldPosition,
                        Quaternion.Euler(0, building[count].buildingData.GetRotationAngle(buildingPreviews[buildingNumber].GetComponent<Building>().direction), 0));
                
                
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
        canBuild = true;
    }
}
