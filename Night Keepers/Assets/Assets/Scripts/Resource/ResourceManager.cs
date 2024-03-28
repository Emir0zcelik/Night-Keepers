using static Building;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    
    public static ResourceManager Instance { get; private set; }
   
    BuildingData buildingData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to add resources
    public void AddResource(ResourceType type, int amount) // Adding resources 
    {
        switch (type)
        {
            case ResourceType.Iron:
                buildingData.iron += amount;
                break;
            case ResourceType.Stone:
                buildingData.stone += amount;
                break;
            case ResourceType.Food:
                buildingData.food += amount;
                break;
            case ResourceType.Wood:
                buildingData.wood += amount;
                break;
        }
    }
}

