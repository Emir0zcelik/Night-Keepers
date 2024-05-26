using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using static BuildingData;


namespace NightKeepers
{
    public class ResourceManagement : MonoBehaviour
    {
        [System.Serializable]
        public class ResourceHave
        {
            public int Wood = 500;
            public int Stone = 500;
            public int Iron = 500;
            public int Food = 500;
        }
        public TMP_Text woodText;
        public TMP_Text foodText;
        public TMP_Text stoneText;

        public ResourceHave resources = new ResourceHave();
        public BuildingData buildingData;
        // public RM rm;

        private int resourceTileCount;

        private bool isProductionStarted = false;
        private Dictionary<string, Coroutine> productionCoroutines = new Dictionary<string, Coroutine>();

        
        public void StartResourceProduction(BuildingData buildingData)
        {
            if (!productionCoroutines.ContainsKey(buildingData.name))
            {
                productionCoroutines[buildingData.name] = StartCoroutine(ProduceResources(buildingData));
            }
            else if (productionCoroutines[buildingData.name] == null)
            {
                productionCoroutines[buildingData.name] = StartCoroutine(ProduceResources(buildingData));
            }
        }

        private void UpdateText()
        {
            woodText.text = resources.Wood.ToString();
            foodText.text = resources.Food.ToString();
            stoneText.text = resources.Stone.ToString();
        }

        private IEnumerator ProduceResources(BuildingData buildingData)
        {
            while (true)
            {
                while (buildingData == null)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(1);
                switch (buildingData.name)
                {
                    case "IronMine":
                        resources.Iron += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                        
                        break;
                    case "StoneMine":
                        resources.Stone += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                        
                        break;
                    case "Farm":
                        resources.Food += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                        
                        break;
                    case "LumberJack":
                        resources.Wood += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                        
                        break;
                    
                    default:
                        break;
                }

            }
        }


        public bool HasEnoughResources()
        {
            if (resources.Wood >= buildingData.Cost.wood &&
                resources.Stone >= buildingData.Cost.stone &&
                resources.Iron >= buildingData.Cost.iron &&
                resources.Food >= buildingData.Cost.food)
            {
            
                DeductResources();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void DeductResources()
        {

            resources.Wood -= buildingData.Cost.wood;
            resources.Stone -= buildingData.Cost.stone;
            resources.Iron -= buildingData.Cost.iron;
            resources.Food -= buildingData.Cost.food;

        }
        public bool HasEnoughResourcesForUnit(ResourceCost cost)
        {
            return resources.Wood >= cost.wood &&
                   resources.Stone >= cost.stone &&
                   resources.Iron >= cost.iron &&
                   resources.Food >= cost.food;
        }

        public void DeductResourcesForUnit(ResourceCost cost)
        {
            resources.Wood -= cost.wood;
            resources.Stone -= cost.stone;
            resources.Iron -= cost.iron;
            resources.Food -= cost.food;
        }

        private void Start()
        {
            buildingData = null;
            if (buildingData != null)
            {
                StartResourceProduction(buildingData);
            }
            
        }
        private void Update()
        {
            UpdateText();
        }

    }
}