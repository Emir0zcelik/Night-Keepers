using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Grid<Tile> _grid;
    private void Start()
    {
        _grid = new Grid<Tile>(10);
    }
    
    
}
