using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;

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

        public List<BuildingData> buildingsData = new List<BuildingData>(); 

        private Dictionary<string, Coroutine> productionCoroutines = new Dictionary<string, Coroutine>();

        private void OnEnable()
        {
            TimeManager.OnNightArrived += StopAllResourceProduction;
            TimeManager.OnDayArrived += StartAllResourceProduction;
            TestBuilding.onBuildingDestroyed += OnBuildingDestroyed;
        }

        private void OnDisable()
        {
            TimeManager.OnNightArrived -= StopAllResourceProduction;
            TimeManager.OnDayArrived -= StartAllResourceProduction;
            TestBuilding.onBuildingDestroyed -= OnBuildingDestroyed;
        }

        private void OnBuildingDestroyed()
        {
            AdjustResourcesAfterBuildingDestroyed();
            /*foreach (var buildingType in RM.Instance.buildingCounts.Keys)
            {
                RestartResourceProduction(buildingType);
            }*/
        }

        private void AdjustResourcesAfterBuildingDestroyed()
        {
           
        }

        private void Start()
        {
            UpdateText();
        }

        private void Update()
        {
            UpdateText();
        }

        public void StartResourceProduction(string buildingType)
        {
            if (!productionCoroutines.ContainsKey(buildingType))
            {
                BuildingData buildingData = RM.Instance.GetBuildingDataByName(buildingType);
                if (buildingData != null)
                {
                    productionCoroutines[buildingType] = StartCoroutine(ProduceResources(buildingData));
                }
            }
        }

        public void RestartResourceProduction(string buildingType)
        {
            if (productionCoroutines.ContainsKey(buildingType))
            {
                StopCoroutine(productionCoroutines[buildingType]);
                productionCoroutines.Remove(buildingType);
            }

            StartResourceProduction(buildingType);
        }

        public void UpdateText()
        {
            woodText.text = resources.Wood.ToString();
            foodText.text = resources.Food.ToString();
            stoneText.text = resources.Stone.ToString();
        }

        private IEnumerator ProduceResources(BuildingData buildingData)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                int totalProductionAmount = RM.Instance.buildingCounts[buildingData.name] * buildingData.ProductionAmount;

                switch (buildingData.buildingTypes)
                {
                    case BuildingData.BuildingType.IronMine:
                        resources.Iron += totalProductionAmount;
                        break;
                    case BuildingData.BuildingType.StoneMine:
                        resources.Stone += totalProductionAmount;
                        break;
                    case BuildingData.BuildingType.Farm:
                        resources.Food += totalProductionAmount;
                        break;
                    case BuildingData.BuildingType.Lumberjack:
                        resources.Wood += totalProductionAmount;
                        break;
                }
            }
        }

        public bool HasEnoughResources(BuildingData buildingData)
        {
            if (resources.Wood >= buildingData.Cost.wood &&
                resources.Stone >= buildingData.Cost.stone &&
                resources.Iron >= buildingData.Cost.iron &&
                resources.Food >= buildingData.Cost.food)
            {
                DeductResources(buildingData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeductResources(BuildingData buildingData)
        {
            resources.Wood -= buildingData.Cost.wood;
            resources.Stone -= buildingData.Cost.stone;
            resources.Iron -= buildingData.Cost.iron;
            resources.Food -= buildingData.Cost.food;
        }

        private void StopAllResourceProduction()
        {
            foreach (var coroutine in productionCoroutines.Values)
            {
                StopCoroutine(coroutine);
            }
            productionCoroutines.Clear();
        }

        private void StartAllResourceProduction()
        {
            foreach (var buildingType in RM.Instance.buildingCounts.Keys)
            {
                StartResourceProduction(buildingType);
            }
        }
    }
}
