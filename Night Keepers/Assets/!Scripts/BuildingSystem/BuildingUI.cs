
using System.Collections;
using System.Collections.Generic;
using NightKeepers;
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
    [SerializeField] private Animator notResearchedAnimation;

    public void MainMenu()
    {
        backButton.SetActive(false);
        resourceBuildingButtons.SetActive(false);
        militartDefenseButtons.SetActive(false);
        generalBuildingButtons.SetActive(false);
        buildingMainMenuButtons.SetActive(true);
    }
    public void GeneralBuildings()
    {
        buildingMainMenuButtons.SetActive(false);
        resourceBuildingButtons.SetActive(false);
        militartDefenseButtons.SetActive(false);
        generalBuildingButtons.SetActive(true);
        backButton.SetActive(true);
        TutorialManager.Instance.isGeneralBuilding = true;
    }

    public void ResourceBuildings()
    {
        buildingMainMenuButtons.SetActive(false);
        militartDefenseButtons.SetActive(false);
        generalBuildingButtons.SetActive(false);
        resourceBuildingButtons.SetActive(true);
        backButton.SetActive(true);
        TutorialManager.Instance.isResourceBuilding = true;
    }

    public void MilitaryDefenseBuildings()
    {
        buildingMainMenuButtons.SetActive(false);
        resourceBuildingButtons.SetActive(false);
        generalBuildingButtons.SetActive(false);
        militartDefenseButtons.SetActive(true);
        backButton.SetActive(true);
        TutorialManager.Instance.isMilitaryBuilding = true;
    }

    public void BackButton()
    {
        generalBuildingButtons.SetActive(false);
        resourceBuildingButtons.SetActive(false);
        militartDefenseButtons.SetActive(false);
        buildingMainMenuButtons.SetActive(true);
        backButton.SetActive(false);
        TutorialManager.Instance.isBackButton = true;
        TutorialManager.Instance.isGeneralBuilding = false;
        TutorialManager.Instance.isResourceBuilding = false;
        TutorialManager.Instance.isMilitaryBuilding = false;
    }

    public void House()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.House);
        BuildingManager.Instance.isBuildingMode = true;
        //notResearchedAnimation.SetBool("shouldPlayAnimation", true);
    }

    public void StorageBuilding()
    {
        // BuildingManager.Instance.SetBuildingType(BuildingType.StorageBuilding);
    }

    public void TownHall()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.TownHall);
        BuildingManager.Instance.isBuildingMode = true;
        //notResearchedAnimation.SetBool("shouldPlayAnimation", true);
    }

    public void ResearchBuilding()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.ResearchBuilding);
        BuildingManager.Instance.isBuildingMode = true;
        //notResearchedAnimation.SetBool("shouldPlayAnimation", true);
    }

    public void LumberJack()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Lumberjack);
        BuildingManager.Instance.isBuildingMode = true;
        //notResearchedAnimation.SetBool("shouldPlayAnimation", true);
    }

    public void Farm()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Farm);
        BuildingManager.Instance.isBuildingMode = true;
        //notResearchedAnimation.SetBool("shouldPlayAnimation", true);
    }

    public void StoneMine()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.StoneMine);
        BuildingManager.Instance.isBuildingMode = true;
        // notResearchedAnimation.SetBool("shouldPlayAnimation", true);
        /*if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.StoneMine))
        {
            BuildingManager.Instance.SetBuildingType(BuildingType.StoneMine);
        }
        else
        notResearchedAnimation.SetBool("shouldPlayAnimation", true);
             */

    }

    public void IronMine()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.IronMine);
        BuildingManager.Instance.isBuildingMode = true;
        /*if (upgrades.unlockedUpgrades.Contains(Upgrades.ResearchUpgrades.IronMine))
         {
             BuildingManager.Instance.SetBuildingType(BuildingType.StoneMine);            
         }
         else
             notResearchedAnimation.SetBool("shouldPlayAnimation", true);*/
    }

    public void FishingHouse()
    {
        BuildingManager.Instance.isBuildingMode = true;
        // BuildingManager.Instance.SetBuildingType(BuildingType.FishingHouse);
    }

    public void Barracks()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Barracks);
        BuildingManager.Instance.isBuildingMode = true;
    }

    public void Walls()
    {
        BuildingManager.Instance.SetBuildingType(BuildingType.Wall);
        BuildingManager.Instance.isBuildingMode = true;
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
        BuildingManager.Instance.isBuildingMode = true;
        // BuildingManager.Instance.SetBuildingType(BuildingType.Traps);
    }

    // public void House()
    // {
    //     buildingManager.SetBuildingType(BuildingData.BuildingType.House);
    // }
}
