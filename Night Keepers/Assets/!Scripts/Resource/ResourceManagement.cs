using System.Collections;
using System.Collections.Generic;
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
        public TMP_Text ironText;

        public ResourceHave resources = new ResourceHave();
        public BuildingData buildingData;
        private Dictionary<string, Coroutine> productionCoroutines = new Dictionary<string, Coroutine>();

        private void OnEnable()
        {
            TimeManager.OnNightArrived += OnNightArrived;
            TimeManager.OnDayArrived += OnDayArrived;
        }

        private void OnDisable()
        {
            TimeManager.OnNightArrived -= OnNightArrived;
            TimeManager.OnDayArrived -= OnDayArrived;
        }

        private void OnNightArrived()
        {
            StopAllResourceProduction();
        }

        private void OnDayArrived()
        {
            StartAllResourceProduction();
        }

        public void StartResourceProduction(BuildingData buildingData)
        {
            if (!productionCoroutines.ContainsKey(buildingData.name))
            {
                productionCoroutines[buildingData.name] = StartCoroutine(ProduceResources(buildingData));
            }
        }

        private void StopAllResourceProduction()
        {
            foreach (var coroutine in productionCoroutines.Values)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
            }
            productionCoroutines.Clear();
        }

        private void StartAllResourceProduction()
        {
            foreach (var building in productionCoroutines.Keys)
            {
                if (productionCoroutines[building] == null)
                {
                    productionCoroutines[building] = StartCoroutine(ProduceResources(buildingData));
                }
            }
        }

        private void UpdateText()
        {
            woodText.text = resources.Wood.ToString();
            foodText.text = resources.Food.ToString();
            stoneText.text = resources.Stone.ToString();
            ironText.text = resources.Iron.ToString();
        }

        private IEnumerator ProduceResources(BuildingData buildingData)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (buildingData != null)
                {
                    switch (buildingData.buildingTypes)
                    {
                        case BuildingData.BuildingType.IronMine:
                            resources.Iron += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                            break;
                        case BuildingData.BuildingType.StoneMine:
                            resources.Stone += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                            break;
                        case BuildingData.BuildingType.Farm:
                            resources.Food += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                            break;
                        case BuildingData.BuildingType.Lumberjack:
                            resources.Wood += buildingData.Workforce * buildingData.ProductionAmount * BuildingManager.Instance.sameTileCount;
                            break;
                        default:
                            break;
                    }
                    UpdateText();
                }
            }
        }

        public bool HasEnoughResources()
        {
            if (buildingData == null)
            {
                Debug.LogError("BuildingData is null in HasEnoughResources method");
                return false;
            }

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


        public void DeductResources()
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
            if (buildingData != null)
            {
                StartResourceProduction(buildingData);
            }
            UpdateText(); // Update text on start
        }

        private void Update()
        {
            UpdateText();
        }
    }
}
