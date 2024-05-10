using NightKeepers.Research;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BuildingData;

public class BuildingUI : MonoBehaviour
{
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
        BuildingManager.Instance.SetBuildingType(BuildingType.House);
    }

    public void StorageBuilding()
    {
        // BuildingManager.Instance.SetBuildingType(BuildingType.StorageBuilding);
    }

    public void TownHall()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.TownHall);
    }

    public void ResearchBuilding()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.ResearchBuilding);
    }

    public void LumberJack()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Lumberjack);
    }

    public void Farm()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Farm);
    }

    public void StoneMine()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.StoneMine);
    }

    public void IronMine()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.IronMine);
    }

    public void FishingHouse()
    {
        // BuildingManager.Instance.SetBuildingType(BuildingType.FishingHouse);
    }

    public void Barracks()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Barracks);
    }

    public void Walls()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Wall);
    }

    public void Traps()
    {
        // BuildingManager.Instance.SetBuildingType(BuildingType.Traps);
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
