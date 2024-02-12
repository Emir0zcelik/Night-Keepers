using NightKeepers;
using NightKeepers.Research;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NightKeepers.Research.Canvas;

namespace NightKeepers.Research
{
    public class Upgrades : MonoBehaviour
    {
        public enum ResearchUpgrades
        {
            MeleeUnitsBuff,
            RangeUnitsBuff,
            BuildingsBuff,
            OthersBuff
        }

        public List<ResearchUpgrades> unlockedUpgrades;
        public Upgrades()
        {
            unlockedUpgrades = new List<ResearchUpgrades>();
        }
        public void UnlockUpgrades(ResearchUpgrades upgrades) 
        {
            unlockedUpgrades.Add(upgrades);
        }
        public bool IsUnlocked(ResearchUpgrades upgrades)
        {
            return unlockedUpgrades.Contains(upgrades);
        }

    }

}





