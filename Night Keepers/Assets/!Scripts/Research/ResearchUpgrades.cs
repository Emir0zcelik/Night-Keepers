using NightKeepers;
using UnityEngine;

public class ResearchUpgrades : MonoBehaviour
{
    public enum ResearchUpgrade
    {
        Lumberjack1,
        Lumberjack2,
        StoneMine,
        Wall,
        Fishing,
        House,
        Trap
    }

    public ResearchUpgrade researchUpgrade;
    public int cost;

    public void ApplyUpgrade()
    {
        switch (researchUpgrade)
        {
            case ResearchUpgrade.Lumberjack1:
                UpgradeLumberjackProduction();
                break;
            case ResearchUpgrade.StoneMine:
                ResearchPointManager.Instance.isStoneMineResearched = true;
                Debug.Log("Stone Mine research completed!");
                break;
            case ResearchUpgrade.Wall:
                ResearchPointManager.Instance.isWallResearched = true;
                Debug.Log("Wall research completed!");
                break;
            case ResearchUpgrade.Fishing:
                ResearchPointManager.Instance.isFishingResearched = true;
                Debug.Log("Wall research completed!");
                break;
            case ResearchUpgrade.House:
                ResearchPointManager.Instance.isHouseResearched = true;
                Debug.Log("House research completed!");
                break;
            case ResearchUpgrade.Trap:
                ResearchPointManager.Instance.isTrapResearched = true;
                Debug.Log("Trap research completed!");
                break;
        }
    }

    private void UpgradeLumberjackProduction()
    {
        
        BuildingData lumberjackData = Resources.Load<BuildingData>("BuildingData/Lumberjack");

        if (lumberjackData != null)
        {
            lumberjackData.ProductionAmount += 1;
            Debug.Log("Lumberjack production increased by 1!");
        }
        else
        {
            Debug.LogError("Lumberjack ScriptableObject not found in Resources folder.");
        }

        
        foreach (var building in RM.Instance.resourceManager.buildingsData)
        {
            if (building.buildingTypes == BuildingData.BuildingType.Lumberjack)
            {
                building.ProductionAmount += 1;
            }
        }

        RM.Instance.resourceManager.RestartResourceProduction("Lumberjack");
    }
}