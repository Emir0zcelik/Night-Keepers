using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tile
{
    public TileType tileType;
    public bool IsFull;
};

public enum TileType{Empty, Rock, Wood, Iron, Water}
