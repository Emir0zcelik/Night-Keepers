using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public void SetBuildingData(BuildingData data)
        {
            if (data == null)
            {
                Debug.LogError("BuildingData is null in SetBuildingData method");
                return;
            }

            resourceManager.buildingData = data;

            if (resourceManager.HasEnoughResources())
            {
                resourceManager.StartResourceProduction(data);
                buildingCounts[data.name]++;

                if (data.name == "IronMine" || data.name == "Lumberjack" || data.name == "Farm" || data.name == "StoneMine")
                {
                    UpdateExistingBuilding(data);
                }
            }
            else
            {
                notEnoughResourceAnimation.SetBool("shouldPlayAnimation", true);
            }
        }


        void UpdateExistingBuilding(BuildingData data)
        {
            int baseProduction = data.ProductionAmount;
            int stackMultiplier = buildingCounts[data.name];
            int newProduction = baseProduction * stackMultiplier;
            data.ProductionAmount = newProduction;
        }
    }
}
