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
            Debug.Log("Awake method called");

            if (buildingsArray == null || buildingsArray.Length == 0)
            {
                Debug.LogError("BuildingsArray is not assigned or empty");
            }

            if (_upgrades == null)
            {
                Debug.LogWarning("Upgrades is not assigned in Awake");
            }

            if (researchText == null)
            {
                Debug.LogError("ResearchText is not assigned");
            }

            for (int i = 0; i < buildingsArray.Length; i++)
            {
                if (buildingsArray[i] == null)
                {
                    Debug.LogError($"BuildingsArray[{i}] is not assigned");
                }
            }

            Debug.Log("Adding button listeners");
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
            Debug.Log($"Attempting to unlock upgrade: {upgrade}");
            if (_upgrades != null && researchText != null)
            {
                _upgrades.TryUnlock(upgrade, researchText);
            }
            else
            {
                Debug.LogError("_upgrades or researchText is null in OnButtonClick");
            }
        }

        public void SetActiveSkills(Upgrades upgrades)
        {
            Debug.Log("SetActiveSkills called");
            this._upgrades = upgrades;
            if (this._upgrades == null)
            {
                Debug.LogError("_upgrades is null in SetActiveSkills");
            }
            else
            {
                Debug.Log("_upgrades assigned successfully in SetActiveSkills");
            }

            BuildingManager.Instance.SetUpgrades(upgrades);

            foreach (var buildUI in builduis)
            {
                buildUI.upgrades = upgrades;
            }
        }
    }
}
