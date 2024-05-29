using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

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
            House,
            Fishing,
            Lumberjack1,
            IronMine,
            StoneMine,
            Wall,
            OthersBuff
        }

        public List<ResearchUpgrades> unlockedUpgrades;
        [SerializeField] private Animator researchPointAnimation;

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
                if (upgrades == ResearchUpgrades.Lumberjack1)
                {
                    UpgradeLumberjackProduction();
                }
            }
        }

        public bool IsUnlocked(ResearchUpgrades upgrades)
        {
            return unlockedUpgrades.Contains(upgrades);
        }

        public ResearchUpgrades GetResearchRequirement(ResearchUpgrades upgrades)
        {
            switch (upgrades)
            {
                
                default:
                    return ResearchUpgrades.None;
            }
        }

        public bool TryUnlock(ResearchUpgrades upgrades, TMP_Text researchText)
        {
            if (researchText == null)
            {
                Debug.LogError("researchText is null");
                return false;
            }

            ResearchUpgrades requirement = GetResearchRequirement(upgrades);
            int currentResearchValue;
            if (!int.TryParse(researchText.text, out currentResearchValue))
            {
                Debug.LogError("Failed to parse researchText");
                return false;
            }
            int requiredResearchPoints = GetRequiredResearchPoints(upgrades);

            if (requirement != ResearchUpgrades.None)
            {
                if (IsUnlocked(requirement))
                {
                    return AttemptUnlock(upgrades, researchText, currentResearchValue, requiredResearchPoints);
                }
                else
                {
                    Debug.Log($"You need to unlock {requirement} first");
                    return false;
                }
            }
            else
            {
                return AttemptUnlock(upgrades, researchText, currentResearchValue, requiredResearchPoints);
            }
        }

        private bool AttemptUnlock(ResearchUpgrades upgrades, TMP_Text researchText, int currentResearchValue, int requiredResearchPoints)
        {
            if (currentResearchValue >= requiredResearchPoints)
            {
                UnlockUpgrades(upgrades);
                researchText.text = (currentResearchValue - requiredResearchPoints).ToString();
                Debug.Log($"Upgrade {upgrades} unlocked!");
                return true;
            }
            else
            {
                if (researchPointAnimation != null)
                {
                    researchPointAnimation.SetBool("shouldPlayAnimation", true);
                }
                Debug.Log("Insufficient ResearchPoints");
                return false;
            }
        }
        private void UpgradeLumberjackProduction()
        {
            
            BuildingData lumberjackData = RM.Instance.buildingsData.FirstOrDefault(building => building.name == "Lumberjack");

            if (lumberjackData != null)
            {
                
                lumberjackData.ProductionAmount += 1;

                
                foreach (var building in RM.Instance.resourceManager.buildingsData)
                {
                    if (building.buildingTypes == BuildingData.BuildingType.Lumberjack)
                    {
                        building.ProductionAmount += 1;
                    }
                }

                
                RM.Instance.resourceManager.RestartResourceProduction("Lumberjack");
            }
            else
            {
                Debug.LogError("Lumberjack building data not found in buildingsData list.");
            }
        }



        private int GetRequiredResearchPoints(ResearchUpgrades upgrade)
        {
            switch (upgrade)
            {
                case ResearchUpgrades.House: return 20;
                case ResearchUpgrades.Fishing: return 50;            
                case ResearchUpgrades.IronMine: return 10;
                case ResearchUpgrades.StoneMine: return 10;
                case ResearchUpgrades.Wall: return 10;
                case ResearchUpgrades.Lumberjack1: return 10;
                default: return 0;
            }
        }
    }
}
