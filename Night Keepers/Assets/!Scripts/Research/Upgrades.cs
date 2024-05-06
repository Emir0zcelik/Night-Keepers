using NightKeepers;
using NightKeepers.Research;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static NightKeepers.Research.Canvas;

namespace NightKeepers.Research
{
    public class Upgrades : MonoBehaviour
    {
        public event EventHandler<OnResearchUnlockedEventArgs> OnResearchUnlocked;
        public class OnResearchUnlockedEventArgs : EventArgs
        {
            public ResearchUpgrades researchUpgrades;
        }
        public enum ResearchUpgrades
        {
            None,
            Lumberjack1,
            Lumberjack2,
            Farm,
            StoneMine,
            IronMine,
            OthersBuff
        }

        public List<ResearchUpgrades> unlockedUpgrades;
        public Upgrades()
        {
            unlockedUpgrades = new List<ResearchUpgrades>();
        }
        public void UnlockUpgrades(ResearchUpgrades upgrades)
        {
             if (!IsUnlocked(upgrades))
             {

                 unlockedUpgrades.Add(upgrades);
                 OnResearchUnlocked?.Invoke(this, new OnResearchUnlockedEventArgs { researchUpgrades = upgrades });
             }
 

        }
        public bool IsUnlocked(ResearchUpgrades upgrades)
        {
            Debug.Log(upgrades);
            Debug.Log(unlockedUpgrades.Count);
            Debug.Log($"List of characters: [{string.Join(", ", unlockedUpgrades)}]");
            return unlockedUpgrades.Contains(upgrades);
        }
        
        public ResearchUpgrades GetResearchRequirement(ResearchUpgrades upgrades)
        {
            switch (upgrades)
            {
                case ResearchUpgrades.Lumberjack2:
                    return ResearchUpgrades.Lumberjack1;              
            }
            return ResearchUpgrades.None;
        }
        public bool TryUnlock(ResearchUpgrades upgrades)
        {
            ResearchUpgrades requirement = GetResearchRequirement(upgrades);
            if (requirement != ResearchUpgrades.None)
            {
                if (IsUnlocked(requirement))
                {
                    UnlockUpgrades(upgrades);
                    return true;
                }
                else
                {
                    Debug.Log("You need to unlock " + requirement + " first");
                    return false;
                }
            }
            else
            {
                UnlockUpgrades(upgrades);
                return true;
            }
        }


    }

}





