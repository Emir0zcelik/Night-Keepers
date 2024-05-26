using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        private Dictionary<string, Coroutine> productionCoroutines = new Dictionary<string, Coroutine>();

        private void OnEnable()
        {
            TimeManager.OnNightArrived += StopAllResourceProduction;
            TimeManager.OnDayArrived += StartAllResourceProduction;
        }

        private void OnDisable()
        {
            TimeManager.OnNightArrived -= StopAllResourceProduction;
            TimeManager.OnDayArrived -= StartAllResourceProduction;
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

        public void StartResourceProduction(BuildingData buildingData)
        {
            if (!productionCoroutines.ContainsKey(buildingData.name))
            {
                productionCoroutines[buildingData.name] = StartCoroutine(ProduceResources(buildingData));
            }
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
                    case "Lumberjack":
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
            if (buildingData != null)
            {
                StartResourceProduction(buildingData);
            }
        }
    }
}
