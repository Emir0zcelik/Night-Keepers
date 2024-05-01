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


        public Button ironMineButton;
        public Button stoneMineButton;
        public Button farmButton;
        public Button lumberJackButton;

        void Start()
        {

            ironMineButton.onClick.AddListener(() => SetBuildingData(ironMineData));
            stoneMineButton.onClick.AddListener(() => SetBuildingData(stoneMineData));
            farmButton.onClick.AddListener(() => SetBuildingData(farmData));
            lumberJackButton.onClick.AddListener(() => SetBuildingData(lumberJackData));
        }

        void SetBuildingData(BuildingData data)
        {
            resourceManager.buildingData = data;
            resourceManager.StartResourceProduction(data);
            resourceManager.HasEnoughResources();
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