using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class RM : Singleton<RM>
    {
        public ResourceManagement resourceManager;
        [SerializeField] private Animator notEnoughResourceAnimation;

        public Dictionary<string, int> buildingCounts = new Dictionary<string, int>();

        void Start()
        {
            buildingCounts["IronMine"] = 0;
            buildingCounts["StoneMine"] = 0;
            buildingCounts["Farm"] = 0;
            buildingCounts["Lumberjack"] = 0;
            buildingCounts["TownHall"] = 0;
            buildingCounts["Test"] = 0;
            buildingCounts["Wall"] = 0;
            buildingCounts["House"] = 0;
            buildingCounts["ResearchBuilding"] = 0;
            buildingCounts["Barrack"] = 0;
            buildingCounts["Fishing"] = 0;
        }

        public void DecreaseBuildingCount(string buildingName)
        {
            if (buildingCounts.ContainsKey(buildingName) && buildingCounts[buildingName] > 0)
            {
                buildingCounts[buildingName]--;
            }
        }

        public void SetBuildingData(BuildingData data)
        {
            if (resourceManager.HasEnoughResources(data))
            {
                buildingCounts[data.name]++;
                resourceManager.StartResourceProduction(data);
            }
        }

        public BuildingData GetBuildingDataByName(string buildingName)
        {
            
            return null; 
        }

        void UpdateExistingBuilding(BuildingData data)
        {
            
        }
    }

    [System.Serializable]
    public class ResourceHave
    {
        public int Wood = 500;
        public int Stone = 500;
        public int Iron = 500;
        public int Food = 500;
    }
}
