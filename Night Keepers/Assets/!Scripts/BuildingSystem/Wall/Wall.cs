using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class Wall : MonoBehaviour
    {
        public Wall UpWall;
        public Wall DownWall;
        public Wall LeftWall;
        public Wall RightWall;

        private MeshFilter meshFilter;

        public bool Up, Down, Left, Right;

        private void Awake() {
            meshFilter = GetComponentInChildren<MeshFilter>();
        }

        private void Start() {

            CheckAllSides();
            
            if (UpWall != null)
            {
                UpWall.CheckAllSides();
            }

            if (DownWall != null)
            {
                DownWall.CheckAllSides();
            }

            if (LeftWall != null)
            {
                LeftWall.CheckAllSides();
            }

            if (RightWall != null)
            {
                RightWall.CheckAllSides();
            }
        }

        public void CheckAllSides()
        {
            Debug.Log("atmaca");
            CheckUp(GridManager.Instance._grid.WorldToGridPosition(transform.position));
            CheckDown(GridManager.Instance._grid.WorldToGridPosition(transform.position));
            CheckLeft(GridManager.Instance._grid.WorldToGridPosition(transform.position));
            CheckRight(GridManager.Instance._grid.WorldToGridPosition(transform.position));
            UpdateWall();
        }
        

        public void CheckUp(Vector2Int gridPosition)
        {
            if (GridManager.Instance._grid[new Vector2Int(gridPosition.x, gridPosition.y + 1)].building != null && GridManager.Instance._grid[new Vector2Int(gridPosition.x, gridPosition.y + 1)].building.buildingType == BuildingData.BuildingType.Wall)
            {
                UpWall = GridManager.Instance._grid[new Vector2Int(gridPosition.x, gridPosition.y + 1)].building.GetComponent<Wall>();
                Up = true;
            }
            else
            {
                Up = false; 
            }
            
            
        }

        public void CheckDown(Vector2Int gridPosition)
        {
            if (GridManager.Instance._grid[new Vector2Int(gridPosition.x, gridPosition.y - 1)].building != null && GridManager.Instance._grid[new Vector2Int(gridPosition.x, gridPosition.y - 1)].building.buildingType == BuildingData.BuildingType.Wall)
            {
                DownWall = GridManager.Instance._grid[new Vector2Int(gridPosition.x, gridPosition.y - 1)].building.GetComponent<Wall>();
                Down = true;
            }
            else
            {
                Down = false; 
            }

        }

        public void CheckLeft(Vector2Int gridPosition)
        {
            if (GridManager.Instance._grid[new Vector2Int(gridPosition.x - 1, gridPosition.y)].building != null && GridManager.Instance._grid[new Vector2Int(gridPosition.x - 1, gridPosition.y)].building.buildingType == BuildingData.BuildingType.Wall)
            {
                LeftWall = GridManager.Instance._grid[new Vector2Int(gridPosition.x - 1, gridPosition.y)].building.GetComponent<Wall>();
                Left = true;
            }
            else
            {
                Left = false; 
            }

        }

        public void CheckRight(Vector2Int gridPosition)
        {
            if (GridManager.Instance._grid[new Vector2Int(gridPosition.x + 1, gridPosition.y)].building != null && GridManager.Instance._grid[new Vector2Int(gridPosition.x + 1, gridPosition.y)].building.buildingType == BuildingData.BuildingType.Wall)
            {
                RightWall = GridManager.Instance._grid[new Vector2Int(gridPosition.x + 1, gridPosition.y)].building.GetComponent<Wall>();
                Right = true;
            }
            else
            {
                Right = false; 
            }

        }

        public void UpdateWall()
        {
            if (Up && Right)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[0].mesh;
            }

            if (Up && Left)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[0].mesh;
                // 180
            }

            if (Down && Right)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[0].mesh;
            }

            if (Down && Left)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[0].mesh;
            }

            if (Up && Right && Left)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[1].mesh;
            }

            if (Up && Right && Down)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[1].mesh;
            }

            if (Right && Down && Left)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[1].mesh;
            }

            if (Down && Left && Up)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[1].mesh;
            }

            if (Up && Down && Right && Left)
            {
                meshFilter.mesh = WallManager.Instance._wallMeshFilters[2].mesh;
            }



        }
    }
}
