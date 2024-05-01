using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NightKeepers
{
    public class RM : MonoBehaviour
    {
        public ResourceManagement resourceManager;

        public BuildingData ironMineData;
        public BuildingData stoneMineData;
        public BuildingData farmData;
        public BuildingData lumberJackData;

        //public int Icount, Lcount, Fcount, Scount;

        public Button ironMineButton;
        public Button stoneMineButton;
        public Button farmButton;
        public Button lumberJackButton;

        public Dictionary<string, int> buildingCounts = new Dictionary<string, int>();

        void Start()
        {
            buildingCounts["IronMine"] = 0;
            buildingCounts["StoneMine"] = 0;
            buildingCounts["Farm"] = 0;
            buildingCounts["LumberJack"] = 0;

            ironMineButton.onClick.AddListener(() => SetBuildingData(ironMineData));
            //ironMineButton.onClick.AddListener(() => Icount++);
            stoneMineButton.onClick.AddListener(() => SetBuildingData(stoneMineData));
           // stoneMineButton.onClick.AddListener(() => Scount++);
            farmButton.onClick.AddListener(() => SetBuildingData(farmData));
           // farmButton.onClick.AddListener(() => Fcount++);
            lumberJackButton.onClick.AddListener(() => SetBuildingData(lumberJackData));
            //lumberJackButton.onClick.AddListener(() => Lcount++);
        }

        void SetBuildingData(BuildingData data)
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
                // Build the building as usual
                resourceManager.buildingData = data;
                resourceManager.StartResourceProduction(data);
                resourceManager.HasEnoughResources();
                // Increase the building count
                buildingCounts[data.name]++;
            }           
        }
        void UpdateExistingBuilding(BuildingData data)
        {
            int baseProduction = data.ProductionAmount; // Base production amount
            int stackMultiplier = buildingCounts[data.name]; // Increase by 1 for each stack
            int newProduction = baseProduction * stackMultiplier;

            // Update the building data with the new production amount
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