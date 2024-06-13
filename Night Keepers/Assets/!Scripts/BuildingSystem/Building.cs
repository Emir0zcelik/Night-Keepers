using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData buildingData;
    public BuildingData.Dir direction;
    public BuildingData.BuildingType buildingType = BuildingData.BuildingType.Empty;

    public int sameTileCount = 0;
    public float deleteCooldown = 0;

}