using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

namespace NightKeepers.Research
{
    public class Canvas : MonoBehaviour
    {
        public  TMP_Text researchText;
        private Upgrades _upgrades;
        private int cost;
        public enum CanvasButtons
        {
            Lumberjack,
            Lumberjack2,
            Farm,
            IronMine,
            StoneMine,
            Others
        }

        void Start()
        {

            researchText.text = researchText.GetComponent<TMP_Text>().text;
            StartCoroutine(UpdateText());
        }

        IEnumerator UpdateText()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                researchText.text = (int.Parse(researchText.text) + Random.Range(1, 4)).ToString();
            }
        }
        private void Awake()
        {
            _upgrades = new Upgrades();
            _upgrades.OnResearchUnlocked += Upgrades_OnResearchUnlocked;
        }

        private void Upgrades_OnResearchUnlocked(object sender, Upgrades.OnResearchUnlockedEventArgs e)
        {
            int currentResearchValue;
            int.TryParse(researchText.text, out currentResearchValue);
            switch (e.researchUpgrades)
            {
                case Upgrades.ResearchUpgrades.Lumberjack1:
                    if(currentResearchValue >= 20)
                    {
                        researchText.text = (currentResearchValue - 20).ToString();
                        Debug.Log("Lumberjack1 = Activated");
                    }
                    else
                    {
                        Debug.Log("Insufficient research value to unlock Lumberjack1.");
                    }
                    break;
                case Upgrades.ResearchUpgrades.Lumberjack2:
                    if(currentResearchValue >= 50)
                    {
                        researchText.text = (currentResearchValue - 50).ToString();
                        Debug.Log("Lumberjack2 = Activated");
                    }
                   else
                    {
                        Debug.Log("Insufficient research value to unlock Lumberjack2.");
                    }
                    break;
                case Upgrades.ResearchUpgrades.Farm:
                    Debug.Log("Farm = Activated");
                    break;
                case Upgrades.ResearchUpgrades.IronMine:
                    Debug.Log("IronMine = Activated");
                    break;
                case Upgrades.ResearchUpgrades.StoneMine:
                    Debug.Log("StoneMine = Activated");
                    break;
                case Upgrades.ResearchUpgrades.OthersBuff:
                    Debug.Log("OthersBuff = Activated");
                    break;

            }
        }

        public Upgrades GetUpgrades()
        {
            return _upgrades;
        }

       

    }
}
