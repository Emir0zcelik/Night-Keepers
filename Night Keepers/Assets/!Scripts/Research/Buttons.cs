using NightKeepers.Research;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NightKeepers.Research.Canvas;
using static UnityEditor.ObjectChangeEventStream;

namespace NightKeepers.Research
{
    public class Buttons : MonoBehaviour
    {
        public Upgrades _upgrades;
        public List<BuildingUI> builduis;
        public TMP_Text researchText;
        public GameObject[] buildingsArray;
        public void Awake()
        {
            buildingsArray[0].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.House, researchText));
            buildingsArray[1].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Fishing, researchText));
            buildingsArray[2].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Farm, researchText));
            buildingsArray[3].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.MainHall, researchText));
            buildingsArray[4].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.StoneMine, researchText));
            buildingsArray[5].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Wall, researchText));
            buildingsArray[6].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Barracks, researchText));
            buildingsArray[7].GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.ResearchBuilding, researchText));
        }

        public void checkarray()
        {

        }

        public void SetActiveSkills(Upgrades upgrades)
        {
            this._upgrades = upgrades;
            foreach(var buildUI in builduis)
                {
                buildUI.upgrades = upgrades;
            }
            
        }
    }
}

