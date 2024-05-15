using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NightKeepers
{
    public class SelectionManager : Singleton<SelectionManager>
    {
        private Transform highlight;
        public static event Action<FunctionalBuilding> onBuildingSelected;  

        private void Update() {

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                Vector2Int gridPosition = GridManager.Instance._grid.WorldToGridPosition(raycastHit.point);

                OutlineSelection(gridPosition);
                SelectedBuilding(gridPosition);
                DeleteBuilding(gridPosition);
            }
        }

        private void SelectedBuilding(Vector2Int gridPosition)
        {
            if (Input.GetMouseButtonDown(0) && GridManager.Instance._grid[gridPosition].building != null)
            {
                if (GridManager.Instance._grid[gridPosition].building.transform.GetChild(0).TryGetComponent<FunctionalBuilding>(out var func))
                {
                    onBuildingSelected?.Invoke(func);
                }
            }
        }

        private void OutlineSelection(Vector2Int gridPosition)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {

                if (GridManager.Instance._grid[gridPosition].building == null)
                {
                    return;
                }

                if (highlight != null)
                {
                    highlight.gameObject.GetComponentInChildren<Outline>().enabled = false;
                    highlight = null;
                }
                

                highlight = GridManager.Instance._grid[gridPosition].building.transform;

                if (highlight.CompareTag("Selectable"))
                {
                    if (highlight.gameObject.GetComponentInChildren<Outline>() != null)
                    {
                        highlight.gameObject.GetComponentInChildren<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = highlight.gameObject.transform.GetChild(0).AddComponent<Outline>();
                        outline.enabled = true;
                        highlight.gameObject.GetComponentInChildren<Outline>().OutlineColor = Color.white;
                        highlight.gameObject.GetComponentInChildren<Outline>().OutlineWidth = 7.0f;
                    }
                }
                else
                {
                    highlight = null;
                }
            }      
        }                  

        public void DeleteBuilding(Vector2Int gridPosition)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (GridManager.Instance._grid[gridPosition].building == null)
                {
                    return;
                }
                if (GridManager.Instance._grid[gridPosition].building.buildingType == BuildingData.BuildingType.TownHall)
                {
                    return;
                }

                Tile tile = new Tile()
                {
                    building = null,
                    tileType = GridManager.Instance._grid[gridPosition].tileType
                };

                if (GridManager.Instance._grid[gridPosition].building != null)
                {
                    Destroy(GridManager.Instance._grid[gridPosition].building.gameObject);
                    foreach (var item in GridManager.Instance._grid[gridPosition].building.buildingData.GetGridPositionList(gridPosition, GridManager.Instance._grid[gridPosition].building.direction))
                    {
                        GridManager.Instance._grid[item] = tile;
                    }
                }                
            }
        }
    }
}
