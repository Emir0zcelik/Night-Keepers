using System.Resources;
using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData buildingData;
    public BuildingData.Dir direction = BuildingData.Dir.Down;
    public BuildingData.BuildingType buildingType = BuildingData.BuildingType.Empty;
    public int productionRate;

 
    private ResourceManager resourceManager;

    public enum ResourceType
    {
        Iron,
        Stone,
        Wood,
        Food
    }

    private void Start()
    {
        
        resourceManager = ResourceManager.Instance;
        StartCoroutine(ProduceResource());
    }

    private IEnumerator ProduceResource() // Coroutine to produce resources in sec DISCOVERY GECILECEK STACKABLE
    {
        while (true)
        {
            switch (buildingType)
            {
                case BuildingData.BuildingType.IronMine:
                    resourceManager.AddResource(ResourceType.Iron, productionRate);
                    break;
                case BuildingData.BuildingType.StoneMine:
                    resourceManager.AddResource(ResourceType.Stone, productionRate);
                    break;
                case BuildingData.BuildingType.Lumberjack:
                    resourceManager.AddResource(ResourceType.Wood, productionRate);
                    break;
                case BuildingData.BuildingType.Farm:
                    resourceManager.AddResource(ResourceType.Food, productionRate);
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
