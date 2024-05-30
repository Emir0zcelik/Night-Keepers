using NightKeepers;
using UnityEngine;

public class ResearchUpgrades : MonoBehaviour
{
    public enum ResearchUpgrade
    {
        Lumberjack1,
        StoneMine,
        Wall,
        Fishing
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
        }
    }

    private void UpgradeLumberjackProduction()
    {
        
        BuildingData lumberjackData = Resources.Load<BuildingData>("BuildingData/Lumberjack");

        if (lumberjackData != null)
        {
            lumberjackData.ProductionAmount += 2;
            Debug.Log("Lumberjack production increased by 2!");
        }
        else
        {
            Debug.LogError("Lumberjack ScriptableObject not found in Resources folder.");
        }

        
        foreach (var building in RM.Instance.resourceManager.buildingsData)
        {
            if (building.buildingTypes == BuildingData.BuildingType.Lumberjack)
            {
                building.ProductionAmount += 2;
            }
        }

        RM.Instance.resourceManager.RestartResourceProduction("Lumberjack");
    }
}
