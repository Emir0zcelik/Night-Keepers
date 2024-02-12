using NightKeepers.Research;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static NightKeepers.Research.Canvas;

namespace NightKeepers.Research
{
    public class Buttons : MonoBehaviour
    {

        [SerializeField] private TMP_Text resourceValue;

        public CanvasButtons buttons;
        private Upgrades _upgrades;
    
        public void OnButtonClick()
        {
            int resourcePoint = int.Parse(resourceValue.text);
            switch (buttons)
            {
                // if the number of text in the Canvas.cs is greater than 10, resourceValue will be reduced by 8.


                case CanvasButtons.MeleeUnits:                                     
                    if (resourcePoint > 8)
                    {
                        resourcePoint -= 8;
                        resourceValue.text = resourcePoint.ToString();
                        _upgrades.UnlockUpgrades(Upgrades.ResearchUpgrades.MeleeUnitsBuff);
                        Debug.Log(_upgrades.unlockedUpgrades.Count);
                    }
                    else Debug.Log("Not enough resource");
                    break;
                case CanvasButtons.RangeUnits:
                    if (resourcePoint > 10)
                    {
                        resourcePoint -= 10;
                        resourceValue.text = resourcePoint.ToString();
                    }
                    else Debug.Log("Not enough resource");                                        
                    break;
                case CanvasButtons.Buildings:
                    if (resourcePoint > 20)
                    {
                        resourcePoint -= 20;
                        resourceValue.text = resourcePoint.ToString();
                    }
                    else Debug.Log("Not enough resource");
                    Debug.Log("Buildings");
                    break;
                case CanvasButtons.Others:
                    if (resourcePoint > 30)
                    {
                        resourcePoint -= 30;
                        resourceValue.text = resourcePoint.ToString();
                    }
                    else Debug.Log("Not enough resource");
                    Debug.Log("Others");
                    break;
            }
        }
        public void SetActiveSkills(Upgrades upgrades)
        {
            this._upgrades = upgrades;
            
        }
    }
}
