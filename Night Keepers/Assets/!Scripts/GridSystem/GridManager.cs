using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Transform _transform;
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
                Vector2Int gridPosition = _grid.WorldToGridPosition(raycastHit.point);
                Debug.Log("grid position: " + gridPosition);
                Instantiate(_transform, _grid.GridToWorldPosition(gridPosition), Quaternion.identity);
            }
            
            
        }
    }
}
