using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

namespace NightKeepers
{
    public class ResourceManagement : MonoBehaviour
    {
        public BuildingData buildingData;

        /*enum ResourceType
        {
            Wood,
            Food,
            Stone,
            Iron
        }*/ //Will be use.

        public int wood, food, stone, iron;
        /*private int stoneMinesCount;
        private int ironMinesCount;
        private int lumberjacksCount;
        private int farmCount;*/  //There will be changes in the future.

        private Dictionary<BuildingData.BuildingType, Coroutine> resourceCoroutines = new Dictionary<BuildingData.BuildingType, Coroutine>();

        private void Start()
        {
            wood = 0;
            food = 0;
            stone = 0;
            iron = 0;

        }

        public void Constructbuild(BuildingData buildingData)
        {
            switch (buildingData.buildingTypes)
            {
                case BuildingData.BuildingType.StoneMine:
                    if (wood >= buildingData.wood && stone >= buildingData.stone)
                    {
                        
                        wood -= buildingData.wood;
                        stone -= buildingData.stone;
                        iron -= buildingData.iron;
                        food -= buildingData.food;
                    }
                    break;

                case BuildingData.BuildingType.IronMine:
                    if (wood >= buildingData.wood && stone >= buildingData.stone)
                    {
                        wood -= buildingData.wood;
                        stone -= buildingData.stone;
                        iron -= buildingData.iron;
                        food -= buildingData.food;
                    }
                    break;

                case BuildingData.BuildingType.Lumberjack:
                    if (wood >= buildingData.wood)
                    {
                        wood -= buildingData.wood;
                        stone -= buildingData.stone;
                        iron -= buildingData.iron;
                        food -= buildingData.food;
                    }
                    break;

                case BuildingData.BuildingType.TownHall:
                    if (wood >= buildingData.wood && stone >= buildingData.stone)
                    {
                        wood -= buildingData.wood;
                        stone -= buildingData.stone;
                        iron -= buildingData.iron;
                        food -= buildingData.food;
                    }
                    break;
            }
            if (!resourceCoroutines.ContainsKey(buildingData.buildingTypes))
            {
                Coroutine coroutine = StartCoroutine(GenerateResource(buildingData));
                resourceCoroutines.Add(buildingData.buildingTypes, coroutine);
            }
        }
        private IEnumerator GenerateResource(BuildingData building)
        {
            while (true)
            {
                yield return new WaitForSeconds(1); 

                
                switch (building.buildingTypes)
                {
                    case BuildingData.BuildingType.StoneMine:
                        stone += building.earn;
                        break;
                    case BuildingData.BuildingType.IronMine:
                        iron += building.earn;
                        break;
                    case BuildingData.BuildingType.Lumberjack:
                        wood += building.earn;
                        break;
                    case BuildingData.BuildingType.TownHall:
                        food += building.earn;
                        break;
                        
                }
            }
        }

        public void StopGeneratingResource(BuildingData building)
        {
            if (resourceCoroutines.TryGetValue(building.buildingTypes, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                resourceCoroutines.Remove(building.buildingTypes);
            }
        }

    }
}
