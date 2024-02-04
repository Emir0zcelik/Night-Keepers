using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingData : ScriptableObject
{
    public new string  name;
    public int health;
    public List<TileType> placableTileTypes;
    
    public int wood;
    public int stone;
    public int iron;
    public int food;
}
