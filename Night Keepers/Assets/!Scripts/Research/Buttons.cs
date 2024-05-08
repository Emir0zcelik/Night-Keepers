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
       // public TMP_Text researchText;

        public void Awake()
        {
                transform.Find("Lumberjack1").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Lumberjack1));
                transform.Find("Lumberjack2").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Lumberjack2));
                transform.Find("Farm").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Farm));
                transform.Find("IronMine").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.IronMine));
                transform.Find("StoneMine").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.StoneMine));
                transform.Find("Wall").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Wall));
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

