using NightKeepers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager>
{
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private WallManager _wallManager;
    [SerializeField] private List<Building> buildings;
    [SerializeField] private List<GameObject> previews; 
    [SerializeField] private List<Building> buildingPreviews; 
    [SerializeField] private List<MeshRenderer> meshRendererPreviews; 
    [SerializeField] private Material validPreviewMaterial;
    [SerializeField] private Material invalidPreviewMaterial;

    public Vector2Int gridPositonForWall;
    public static event Action<GameObject> OnMainBuildingPlaced;

    public List<Building> walls;
    private BuildingData.BuildingType buildingType;

    private Vector2Int gridPosition;
    private bool isRotated = false;

    public RM rm;

    private int buildingNumber;
    bool isPlaced = false;

    bool isPlaceBuilding = false;

    bool isBuildingMode = true;

    //private int sameTileCount = 0;
    public int sameTileCount { get; private set; }

    // Dictionary<int, List<Material>> Materi
    
    private void Awake() {
        foreach (var preview in previews)
        {
            buildingPreviews.Add(preview.GetComponentInChildren<Building>());
        }
        int i = 0;
        foreach (var buildingPreview in buildingPreviews)
        {
            meshRendererPreviews.Add(buildingPreviews[i++].GetComponentInChildren<MeshRenderer>());
        }
    }

    private void Start() {
        foreach (var preview in previews)
        {
            preview.transform.localScale = new Vector3(preview.transform.localScale.x * _gridManager.cellSize / 10, preview.transform.localScale.y * _gridManager.cellSize / 10, preview.transform.localScale.z * _gridManager.cellSize / 10);
            preview.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        SelectBuilding();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
        {
            gridPosition = _gridManager._grid.WorldToGridPosition(raycastHit.point);
            if (isBuildingMode)
            {                
                isPlaceBuilding = true;
                PreviewBuilding(gridPosition);
            }
            else
            {
                previews[buildingNumber].transform.position = Vector3.zero;
            }
        }
        else{
            isPlaceBuilding = false;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isRotated = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isBuildingMode = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            isBuildingMode = true;
        }

        if (isPlaceBuilding && isBuildingMode)
        {
            PlaceBuilding(gridPosition);
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
            case BuildingData.BuildingType.Wall:
                isPlaced = false;
                buildingNumber = 5;
                BuildingPreviewsActivate(buildingNumber);
                break;
            case BuildingData.BuildingType.House:
                isPlaced = false;
                buildingNumber = 6;
                BuildingPreviewsActivate(buildingNumber);
                break;
        }

    }

    private void BuildingPreviewsActivate(int active)
    {
        for (int i = 0; i < previews.Count; i++)
        {
            if (i == active)
            {
                previews[i].SetActive(true);
            }
            else
            {
                previews[i].SetActive(false);
            }

        }
    }

    private void PreviewBuilding(Vector2Int gridPosition)
    {
        if(!isPlaced)
        {
            if (TryBuild(buildings[buildingNumber], buildings[buildingNumber].buildingData.GetGridPositionList(gridPosition, buildings[buildingNumber].direction),rm))
            {   
                var yourMaterials = new Material[]
                {
                    validPreviewMaterial, validPreviewMaterial
                };

                meshRendererPreviews[buildingNumber].materials = yourMaterials;
            }
            else
            {
                var yourMaterials = new Material[]
                {
                    invalidPreviewMaterial, invalidPreviewMaterial
                };

                meshRendererPreviews[buildingNumber].materials = yourMaterials;        
            }
            
            Vector2Int rotationOffset = buildingPreviews[buildingNumber].buildingData.GetRotationOffset(buildingPreviews[buildingNumber].direction);
            Vector3 instantiatedBuildingWorldPosition = _gridManager._grid.GridToWorldPosition(gridPosition) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * _gridManager.cellSize;
            previews[buildingNumber].transform.position = instantiatedBuildingWorldPosition;
            if (isRotated)
            {
                buildingPreviews[buildingNumber].direction = BuildingData.GetNextDir(buildingPreviews[buildingNumber].direction);
                previews[buildingNumber].transform.rotation = Quaternion.Euler(0, buildingPreviews[buildingNumber].buildingData.GetRotationAngle(buildingPreviews[buildingNumber].direction), 0);                
                isRotated = false;
            }
        }    
    }

    public Vector2Int GetPreviewPosition()
    {
        return _gridManager._grid.WorldToGridPosition(previews[buildingNumber].transform.position);
    }

    private void PlaceBuilding(Vector2Int gridPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // if (buildings[buildingNumber].buildingData.widthHeight.x == buildings[buildingNumber].buildingData.widthHeight.y)
            // {

            // }
            // if (buildings[buildingNumber].buildingData.widthHeight.x > buildings[buildingNumber].buildingData.widthHeight.y)
            // {
                
            // }
            // if (buildings[buildingNumber].buildingData.widthHeight.x < buildings[buildingNumber].buildingData.widthHeight.y)
            // {
                
            // }
            List<Vector2Int> gridPositionList = buildings[buildingNumber].buildingData.GetGridPositionList(gridPosition, buildings[buildingNumber].direction);
            buildings[buildingNumber].transform.position = _gridManager._grid.GridToWorldPosition(gridPosition);


            if (TryBuild(buildings[buildingNumber], gridPositionList,rm))
            {                
                Building instantiatedBuilding = Instantiate(
                        buildings[buildingNumber],
                        previews[buildingNumber].transform.position,
                        Quaternion.Euler(0, buildings[buildingNumber].buildingData.GetRotationAngle(buildingPreviews[buildingNumber].direction), 0));


                foreach (Vector2Int position in gridPositionList)
                {
                    Tile tile = new Tile()
                    {
                        building = instantiatedBuilding,
                        tileType = _gridManager._grid[gridPosition].tileType,
                    };
                    _gridManager._grid[position] = tile;
                }

                if (buildingNumber == 5)
                {
                    SetGridPositionForWall(_gridManager._grid.WorldToGridPosition(instantiatedBuilding.transform.position));
                }

                if (buildingNumber == 3)
                {
                    OnMainBuildingPlaced?.Invoke(instantiatedBuilding.gameObject);
                }

                foreach (var item in gridPositionList)
                {
                    print(item);
                }
            }
        }
        
    }

    public void SetGridPositionForWall(Vector2Int gridPosition)
    {
        gridPositonForWall = gridPosition;
    }

    public void SetBuildingType(BuildingData.BuildingType buildingType)
    {
        this.buildingType = buildingType;
    }

    

    private bool TryBuild(Building building, List<Vector2Int> gridPositionList, RM rmInstance)
    {
        sameTileCount = 0;
        foreach (Vector2Int position in gridPositionList)
        {
            if (_gridManager._grid[position].building != null)
            {
                // Debug.Log("Null deil");
                return false; 
            }
            if (building.buildingData.placableTileTypes[1] == _gridManager._grid[position].tileType)
            {
                // Debug.Log("su degil");
                return false;
            }

            if (building.buildingData.placableTileTypes[0] == _gridManager._grid[position].tileType)
            {
                // Debug.Log("same tile count > 0");
                sameTileCount++;
            }
        }

        if (sameTileCount == 0)
        {
            // Debug.Log("same tile count == 0");
            return false;
        }

        isPlaced = true;

        if (Input.GetMouseButtonDown(0))
        {
            rmInstance.SetBuildingData(building.buildingData);
        }
        

        return true;
    }
}
