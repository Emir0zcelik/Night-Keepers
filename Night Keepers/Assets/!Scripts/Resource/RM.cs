using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NightKeepers
{
    public class RM : Singleton<RM>
    {
        public ResourceManagement resourceManager;
        [SerializeField] private Animator notEnoughResourceAnimation;

        public Dictionary<string, int> buildingCounts = new Dictionary<string, int>();

        
        public List<BuildingData> buildingsData;

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

            
            buildingsData = new List<BuildingData>(Resources.LoadAll<BuildingData>("BuildingData"));
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
                if (!buildingCounts.ContainsKey(data.name))
                {
                    buildingCounts[data.name] = 0;
                }
                buildingCounts[data.name]++;
                resourceManager.StartResourceProduction(data.name);
            }
        }

        public BuildingData GetBuildingDataByName(string buildingName)
        {
            return buildingsData.FirstOrDefault(bd => bd.name == buildingName);
        }
    }
}
