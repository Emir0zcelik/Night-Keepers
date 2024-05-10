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
            buildingCounts["LumberJack"] = 0;
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
            if (data.name == "IronMine" && buildingCounts[data.name] >= 1)
            {
                buildingCounts[data.name]++;
                UpdateExistingBuilding(data);
            }
            if (data.name == "LumberJack" && buildingCounts[data.name] >= 1)
            {
                buildingCounts[data.name]++;
                UpdateExistingBuilding(data);
            }
            if (data.name == "Farm" && buildingCounts[data.name] >= 1)
            {
                buildingCounts[data.name]++;
                UpdateExistingBuilding(data);
            }
            if (data.name == "StoneMine" && buildingCounts[data.name] >= 1)
            {
                buildingCounts[data.name]++;
                UpdateExistingBuilding(data);
            }
            else
            {

                resourceManager.buildingData = data;
                //resourceManager.HasEnoughResources();
                resourceManager.StartResourceProduction(data);
                if (resourceManager.HasEnoughResources())
                {
                    buildingCounts[data.name]++;
                }


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
    [System.Serializable]
    public class ResourceHave
    {
        public int Wood = 500;
        public int Stone = 500;
        public int Iron = 500;
        public int Food = 500;
    }

}