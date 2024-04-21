using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string, int> resources = new Dictionary<string, int>();
    private Dictionary<string, int> resourceLimits = new Dictionary<string, int>();
    public TextMeshProUGUI resourcesText;

    void Start()
    {
        AddResource("Wood", 50, 10000); // Baþlangýç deðeri ve limiti ile Wood 
        AddResource("Stone", 30, 5000);
        AddResource("Population", 10, 2500);
        AddResource("Iron", 20, 4000);
        AddResource("ResearchPoint", 100, 50000);
        AddResource("Food", 100, 20000);
    }

    void Update()
    {
        // Wood eklemek için "W" 
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddResource("Wood", 10, resourceLimits["Wood"]);
            Debug.Log("Wood Added. Current Wood: " + GetResourceAmount("Wood"));
        }

        // Stone eklemek için "S" 
        if (Input.GetKeyDown(KeyCode.S))
        {
            AddResource("Stone", 5, resourceLimits["Stone"]);
            Debug.Log("Stone Added. Current Stone: " + GetResourceAmount("Stone"));
        }

        // Wood harcamak için "D"   
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (SpendResource("Wood", 20))
            {
                Debug.Log("Wood Spent. Current Wood: " + GetResourceAmount("Wood"));
            }
            else
            {
                Debug.Log("Not enough Wood to spend.");
            }
        }

        // Stone harcamak için "F"  
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (SpendResource("Stone", 10))
            {
                Debug.Log("Stone Spent. Current Stone: " + GetResourceAmount("Stone"));
            }
            else
            {
                Debug.Log("Not enough Stone to spend.");
            }
        }

        // Wood limitini artýrmak için "R" 
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpdateResourceLimit("Wood", resourceLimits["Wood"] + 50);
            Debug.Log("Wood limit increased. New limit: " + resourceLimits["Wood"]);
        }

        // Stone limitini artýrmak için "T" 
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpdateResourceLimit("Stone", resourceLimits["Stone"] + 25);
            Debug.Log("Stone limit increased. New limit: " + resourceLimits["Stone"]);
        }
    }

    public void AddResource(string name, int amount, int limit)
    {
        if (resources.ContainsKey(name))
        {
            resources[name] += amount;
            if (resources[name] > resourceLimits[name])
                resources[name] = resourceLimits[name];
        }
        else
        {
            resources[name] = amount;
            resourceLimits[name] = limit;
        }
    }

    public bool SpendResource(string name, int amount)
    {
        if (!resources.ContainsKey(name) || resources[name] < amount)
        {
            return false;
        }
        else
        {
            resources[name] -= amount;
            return true;
        }
    }

    public void UpdateResourceLimit(string name, int newLimit)
    {
        if (resourceLimits.ContainsKey(name))
        {
            resourceLimits[name] = newLimit;
            if (resources[name] > newLimit)
            {
                resources[name] = newLimit;
            }
        }
        else
        {
            Debug.LogError("UpdateResourceLimit: Kaynak bulunamadý - " + name);
        }
    }

    public int GetResourceAmount(string name)
    {
        if (resources.ContainsKey(name))
        {
            return resources[name];
        }
        else
        {
            Debug.LogError("GetResourceAmount: Kaynak bulunamadý - " + name);
            return 0;
        }
    }
}
