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
                Debug.Log($"Building count for {buildingName} is now {buildingCounts[buildingName]}");
            }
            else
                Debug.LogWarning($"Building count for {buildingName} could not be decreased. Current count: {buildingCounts.GetValueOrDefault(buildingName, 0)}");
        }

        public void SetBuildingData(BuildingData data)
        {
            
                if (!buildingCounts.ContainsKey(data.name))
                {
                    buildingCounts[data.name] = 0;
                }
                buildingCounts[data.name]++;
                resourceManager.StartResourceProduction(data.name);
            
        }

        public BuildingData GetBuildingDataByName(string buildingName)
        {
            return buildingsData.FirstOrDefault(bd => bd.name == buildingName);
        }
    }
}
