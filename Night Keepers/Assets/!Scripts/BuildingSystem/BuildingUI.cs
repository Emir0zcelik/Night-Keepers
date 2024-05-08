using NightKeepers.Research;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuildingData;
using static NightKeepers.Research.Upgrades;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] BuildingManager buildingManager;
    public Upgrades upgrades;
    public void Lumberjack()
    {       
        Debug.Log($"List of characters: [{string.Join(", ", upgrades.unlockedUpgrades)}]");
       
        if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.Lumberjack1))
        {
            buildingManager.SetBuildingType(BuildingType.Lumberjack1);
            Debug.Log("LB1");
        }
        else
        {
            buildingManager.SetBuildingType(BuildingType.Lumberjack);
            Debug.Log("LB");
        }
    }
    public void StoneMine()
    {
        if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.StoneMine))
        {
            buildingManager.SetBuildingType(BuildingData.BuildingType.StoneMine);
            Debug.Log("STONEMINE");
        }
        else
            Debug.Log("Cannot build StoneMine, Research first!");
    }

    public void IronMine()
    {
        if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.IronMine))
        {
            buildingManager.SetBuildingType(BuildingData.BuildingType.IronMine);
            Debug.Log("IRONMINE");
        }
        else
            Debug.Log("Cannot build IronMine, Research first!");
        
        Debug.Log("IRONMINE");
    }

    public void TownHall()
    {
        buildingManager.SetBuildingType(BuildingData.BuildingType.TownHall);
        Debug.Log("TOWNHALL");
    }

    public void Test()
    {
        buildingManager.SetBuildingType(BuildingData.BuildingType.Test);
        Debug.Log("TEST");
    }

}
