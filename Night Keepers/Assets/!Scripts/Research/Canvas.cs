using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

namespace NightKeepers.Research
{
    public class Canvas : MonoBehaviour
    {
        public  TMP_Text text;
        private Upgrades _upgrades;

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

            text.text = text.GetComponent<TMP_Text>().text;
            StartCoroutine(UpdateText());
        }

        IEnumerator UpdateText()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                text.text = (int.Parse(text.text) + Random.Range(1, 4)).ToString();
            }
        }
        private void Awake()
        {
            _upgrades = new Upgrades();
            _upgrades.OnResearchUnlocked += Upgrades_OnResearchUnlocked;
        }

        private void Upgrades_OnResearchUnlocked(object sender, Upgrades.OnResearchUnlockedEventArgs e)
        {
            switch (e.researchUpgrades)
            {
                case Upgrades.ResearchUpgrades.Lumberjack1:
                    Debug.Log("Lumberjack1 = Activated");
                    break;
                case Upgrades.ResearchUpgrades.Lumberjack2: 
                    Debug.Log("Lumberjack2 = Activated");
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

        public bool Lumberjack1Active()
        {
            return _upgrades.IsUnlocked(Upgrades.ResearchUpgrades.Lumberjack1);
        }
        public bool Lumberjack2Active()
        {
            return _upgrades.IsUnlocked(Upgrades.ResearchUpgrades.Lumberjack2);
        }
        public bool FarmActive()
        {
            return _upgrades.IsUnlocked(Upgrades.ResearchUpgrades.Farm);
        }
        public bool IronMineActive()
        {
            return _upgrades.IsUnlocked(Upgrades.ResearchUpgrades.IronMine);
        }
        public bool StoneMineActive()
        {
            return _upgrades.IsUnlocked(Upgrades.ResearchUpgrades.StoneMine);
        }

    }
}
