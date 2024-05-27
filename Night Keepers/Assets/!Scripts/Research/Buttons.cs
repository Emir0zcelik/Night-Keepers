using NightKeepers;
using NightKeepers.Research;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NightKeepers.Research
{
    public class Buttons : MonoBehaviour
    {
        public Upgrades _upgrades;
        public List<BuildingUI> builduis;
        public TMP_Text researchText;
        public GameObject[] buildingsArray;

        private void Awake()
        {
            buildingsArray[0]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.House));
            buildingsArray[1]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.Fishing));
            buildingsArray[2]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.Farm));
            buildingsArray[3]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.MainHall));
            buildingsArray[4]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.StoneMine));
            buildingsArray[5]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.Wall));
            buildingsArray[6]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.Barracks));
            buildingsArray[7]?.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(Upgrades.ResearchUpgrades.ResearchBuilding));
        }

        private void OnButtonClick(Upgrades.ResearchUpgrades upgrade)
        {
            if (_upgrades != null && researchText != null)
            {
                if (upgrade != Upgrades.ResearchUpgrades.Barracks && upgrade != Upgrades.ResearchUpgrades.ResearchBuilding)
                {
                    _upgrades.TryUnlock(upgrade, researchText);
                }
            }
        }

        public void SetActiveSkills(Upgrades upgrades)
        {
            this._upgrades = upgrades;

            BuildingManager.Instance.SetUpgrades(upgrades);

            foreach (var buildUI in builduis)
            {
                buildUI.upgrades = upgrades;
            }
        }
    }
}
