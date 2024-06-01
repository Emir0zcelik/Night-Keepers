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

                if (GridManager.Instance._grid.IsInDimensions(gridPosition))
                {
                    //OutlineSelection(gridPosition);
                    SelectedBuilding(gridPosition);           
                }

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

                if (GridManager.Instance._grid[gridPosition].building == null || GridManager.Instance._grid[gridPosition].building.buildingType == BuildingData.BuildingType.Environment)
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
                    highlight.gameObject.GetComponentInChildren<Outline>().enabled = false;
                }
                highlight = null;
            }
        }               
    }
}
