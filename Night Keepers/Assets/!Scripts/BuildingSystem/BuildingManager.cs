using NightKeepers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : Singleton<BuildingManager>
{
    [SerializeField] private List<Building> buildings;
    [SerializeField] private List<GameObject> previews; 
    private List<Building> buildingPreviews = new List<Building>(); 
    private List<MeshRenderer> meshRendererPreviews = new List<MeshRenderer>(); 
    [SerializeField] private Material validPreviewMaterial;
    [SerializeField] private Material invalidPreviewMaterial;
    public static event Action<GameObject> OnMainBuildingPlaced;
    private BuildingData.BuildingType buildingType;
    private Vector2Int gridPosition;
    private bool isRotated = false;
    private int buildingNumber;
    public bool isPlaced = false;
    bool isPlaceBuilding = false;
    bool isBuildingMode = true;
    public bool isTownHallPlaced = false;
    public int sameTileCount { get; private set; }

    public static event Action OnBuildingPlaced;

    [SerializeField] private LayerMask layerMask;
    
    protected override void Awake() {
        base.Awake();
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
            preview.transform.localScale = new Vector3(preview.transform.localScale.x * GridManager.Instance.cellSize / 10, preview.transform.localScale.y * GridManager.Instance.cellSize / 10, preview.transform.localScale.z * GridManager.Instance.cellSize / 10);
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
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, layerMask))
        {
            gridPosition = GridManager.Instance._grid.WorldToGridPosition(raycastHit.point);
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

            case BuildingData.BuildingType.Farm:
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
            case BuildingData.BuildingType.ResearchBuilding:
                isPlaced = false;
                buildingNumber = 7;
                BuildingPreviewsActivate(buildingNumber);
                break;
            case BuildingData.BuildingType.Barracks:
                isPlaced = false;
                buildingNumber = 8;
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
            if (TryBuild(buildings[buildingNumber], buildings[buildingNumber].buildingData.GetGridPositionList(gridPosition, buildings[buildingNumber].direction),RM.Instance))
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
            Vector3 instantiatedBuildingWorldPosition = GridManager.Instance._grid.GridToWorldPosition(gridPosition) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * GridManager.Instance.cellSize;
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
        return GridManager.Instance._grid.WorldToGridPosition(previews[buildingNumber].transform.position);
    }

    private void PlaceBuilding(Vector2Int gridPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<Vector2Int> gridPositionList = buildings[buildingNumber].buildingData.GetGridPositionList(gridPosition, buildingPreviews[buildingNumber].direction);
            buildings[buildingNumber].transform.position = GridManager.Instance._grid.GridToWorldPosition(gridPosition);
            
            if (isTownHallPlaced || (!isTownHallPlaced && buildingNumber == 3))
            {
                if (TryBuild(buildings[buildingNumber], gridPositionList,RM.Instance))
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
                            tileType = GridManager.Instance._grid[gridPosition].tileType,
                        };
                        GridManager.Instance._grid[position] = tile;
                    }

                    OnBuildingPlaced?.Invoke();

                    if (buildingNumber == 3)
                    {
                        isTownHallPlaced = true;
                        TimeManager.Instance.isTimeStarted = true;
                        OnMainBuildingPlaced?.Invoke(instantiatedBuilding.gameObject);
                    }
                }
            }
        }
        
    }

    public void SetBuildingType(BuildingData.BuildingType buildingType)
    {
        this.buildingType = buildingType;
    }

    

    private bool TryBuild(Building building, List<Vector2Int> gridPositionList, RM rmInstance)
    {
        int localSameTileCount = 0;
        foreach (Vector2Int position in gridPositionList)
        {
            if (GridManager.Instance._grid[position].building != null)
            {
                return false; 
            }
            if (building.buildingData.placableTileTypes[1] == GridManager.Instance._grid[position].tileType)
            {
                return false;
            }

            if (building.buildingData.placableTileTypes[0] == GridManager.Instance._grid[position].tileType)
            {
                localSameTileCount++;
            }
        }

        if (buildingNumber == 3 && isTownHallPlaced)
        {
            return false;
        } 
                 
        if (localSameTileCount == 0)
        {
            return false;
        }


        isPlaced = true;
        if (Input.GetMouseButtonDown(0))
        {
            rmInstance.SetBuildingData(building.buildingData);
            BuildingManager.Instance.sameTileCount = localSameTileCount;
        }
        

        return true;
    }
}
