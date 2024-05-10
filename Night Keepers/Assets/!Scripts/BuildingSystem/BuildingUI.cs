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
    //[SerializeField] private Animator notEnoughResourceAnimation;

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
        /*if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.StoneMine))
        {
            BuildingManager.Instance.SetBuildingType(BuildingType.StoneMine);
        }
        else
            notEnoughResourceAnimation.Play("Resourcenotenough");*/
     
    }

    public void IronMine()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.IronMine);
        /*if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.IronMine))
         {
             BuildingManager.Instance.SetBuildingType(BuildingType.StoneMine);            
         }
         else
             notEnoughResourceAnimation.Play("Resourcenotenough");*/
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
        /* if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.Wall))
              {
             BuildingManager.Instance.SetBuildingType(BuildingType.Wall);
             Debug.Log("WALL");
              }
           else
               notEnoughResourceAnimation.Play("Resourcenotenough");;*/
    }

    public void Traps()
    {
        // BuildingManager.Instance.SetBuildingType(BuildingType.Traps);
    }

    // public void House()
    // {
    //     buildingManager.SetBuildingType(BuildingData.BuildingType.House);
    // }
}
