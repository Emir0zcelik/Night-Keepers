using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


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
        public ResourceHave resources = new ResourceHave();
        public BuildingManager buildingManager;
        public BuildingData buildingData;
        public RM rm;

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
                        resources.Iron += buildingData.Workforce * buildingData.ProductionAmount * buildingManager.sameTileCount;
                        
                        break;
                    case "StoneMine":
                        resources.Stone += buildingData.Workforce * buildingData.ProductionAmount * buildingManager.sameTileCount;
                        
                        break;
                    case "Farm":
                        resources.Food += buildingData.Workforce * buildingData.ProductionAmount * buildingManager.sameTileCount;
                        
                        break;
                    case "LumberJack":
                        resources.Wood += buildingData.Workforce * buildingData.ProductionAmount * buildingManager.sameTileCount;
                        
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
        private void Start()
        {
            buildingData = null;
            if (buildingData != null)
            {
                StartResourceProduction(buildingData);
            }

        }

    }
}