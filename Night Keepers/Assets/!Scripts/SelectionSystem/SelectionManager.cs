using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace NightKeepers
{
    public class SelectionManager : Singleton<SelectionManager>
    {

        private void Update() {

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                Vector2Int gridPosition = GridManager.Instance._grid.WorldToGridPosition(raycastHit.point);

                DeleteBuilding(gridPosition);
            }
        }

        private void OutlineSelection(Vector2Int gridPosition)
        {
            if (!GridManager.Instance._grid[gridPosition].building.CompareTag("Selectable"))
                return;

            GridManager.Instance._grid[gridPosition].building.gameObject.GetComponent<Outline>().enabled = true;

            
        }                    

        private void DeleteBuilding(Vector2Int gridPosition)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Tile tile = new Tile()
                {
                    building = null,
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
