using NightKeepers.Research;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BuildingData;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] BuildingManager buildingManager;
    [SerializeField] private GameObject buildingMainMenuButtons;
    [SerializeField] private GameObject generalBuildingButtons;
    [SerializeField] private GameObject resourceBuildingButtons;
    [SerializeField] private GameObject militartDefenseButtons;
    [SerializeField] private GameObject backButton;

    public Upgrades upgrades;

    public void GeneralBuildings()
    {
        buildingMainMenuButtons.SetActive(false);
        resourceBuildingButtons.SetActive(false);
        militartDefenseButtons.SetActive(false);
        generalBuildingButtons.SetActive(true);
        backButton.SetActive(true);
    }

    public void ResourceBuildings()
    {
        buildingMainMenuButtons.SetActive(false);
        militartDefenseButtons.SetActive(false);
        generalBuildingButtons.SetActive(false);
        resourceBuildingButtons.SetActive(true);
        backButton.SetActive(true);
    }

    public void MilitaryDefenseBuildings()
    {
        buildingMainMenuButtons.SetActive(false);
        resourceBuildingButtons.SetActive(false);
        generalBuildingButtons.SetActive(false);
        militartDefenseButtons.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackButton()
    {
        generalBuildingButtons.SetActive(false);
        resourceBuildingButtons.SetActive(false);
        militartDefenseButtons.SetActive(false);
        buildingMainMenuButtons.SetActive(true);
        backButton.SetActive(false);
    }

    public void House()
    {
        Debug.Log("House Selected");
    }

    public void StorageBuilding()
    {
        Debug.Log("Storage Building Selected");
    }

    public void TownHall()
    {
        Debug.Log("Town Hall Selected");
    }

    public void ResearchBuilding()
    {
        Debug.Log("Research Building Selected");
    }

    public void LumberJack()
    {
        Debug.Log("Lumberjack Building Selected");
    }

    public void Farm()
    {
        Debug.Log("Farm Building Selected");
    }

    public void StoneMine()
    {
        Debug.Log("Stone Mine Building Selected");
    }

    public void IronMine()
    {
        Debug.Log("Iron Mine Building Selected");
    }

    public void FishingHouse()
    {
        Debug.Log("Fishing House Building Selected");
    }

    public void Barracks()
    {
        Debug.Log("Barracks Building Selected");
    }

    public void Walls()
    {
        Debug.Log("Wall Building Selected");
    }

    public void Traps()
    {
        Debug.Log("Traps Building Selected");
    }


    
    
    // public void Lumberjack()
    // {
    //      if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.Lumberjack2))
    //      {
    //          buildingManager.SetBuildingType(BuildingType.Lumberjack2);
    //          Debug.Log("LB2");
    //      }
    //      if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.Lumberjack1))
    //      {
    //          buildingManager.SetBuildingType(BuildingType.Lumberjack1);
    //          Debug.Log("LB1");
    //      }
    //      else
    //      {
    //         buildingManager.SetBuildingType(BuildingType.Lumberjack);
    //          Debug.Log("LB");
    //      }

    // }
    // public void StoneMine()
    // {
    //      if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.StoneMine))
    //      {
    //         buildingManager.SetBuildingType(BuildingData.BuildingType.StoneMine);
    //          Debug.Log("STONEMINE");
    //      }
    //      else
    //          Debug.Log("Cannot build StoneMine, Research first!");
    // }

    // public void IronMine()
    // {
    //     if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.IronMine))
    //     {
    //         buildingManager.SetBuildingType(BuildingData.BuildingType.IronMine);
    //         Debug.Log("IRONMINE");
    //     }
    //     else
    //         Debug.Log("Cannot build IronMine, Research first!");
    // }

    // public void TownHall()
    // {
    //     buildingManager.SetBuildingType(BuildingData.BuildingType.TownHall);
    // }

    // public void Test()
    // {
    //     buildingManager.SetBuildingType(BuildingData.BuildingType.Test);
    // }

    // public void Wall()
    // {

    //     if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.Wall))
    //         {
    //         buildingManager.SetBuildingType(BuildingData.BuildingType.Wall);
    //         Debug.Log("WALL");
    //         }
    //      else
    //          Debug.Log("Cannot build Wall, Research first!");        
    // }

    // public void House()
    // {
    //     buildingManager.SetBuildingType(BuildingData.BuildingType.House);
    // }
}
