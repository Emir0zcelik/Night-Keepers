using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    private int woodAmount;
    private int woodLimit;

    void Start()
    {
        SetWood(0);
        SetWoodLimit(10000);
    }

    public void SetWood(int amount)
    {
        woodAmount = Mathf.Clamp(amount, 0, woodLimit); 
    }

    public void AddWood(int amount)
    {
        SetWood(woodAmount + amount);
    }

    public bool SpendWood(int amount)
    {
        if (woodAmount >= amount)
        {
            SetWood(woodAmount - amount);
            return true;
        }
        else
        {
            Debug.LogWarning("Insufficient Wood: You cannot spend the requested amount.");
            return false;
        }
    }

    public void SetWoodLimit(int limit)
    {
        woodLimit = Mathf.Max(0, limit); 
        if (woodAmount > woodLimit)
        {
            SetWood(woodLimit); 
        }
    }

    public int GetWoodAmount()
    {
        return woodAmount;
    }

    public int GetWoodLimit()
    {
        return woodLimit;
    }
}
