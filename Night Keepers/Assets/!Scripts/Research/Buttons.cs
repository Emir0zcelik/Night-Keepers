using NightKeepers.Research;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static NightKeepers.Research.Canvas;

namespace NightKeepers.Research
{
    public class Buttons : MonoBehaviour
    {

        //[SerializeField] TMP_Text resourceValue;
        public Upgrades _upgrades;
        public BuildingUI buildui;

        public void Awake()
        {

            transform.Find("Lumberjack1").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Lumberjack1));
            transform.Find("Lumberjack2").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Lumberjack2));
            transform.Find("Farm").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.Farm));
            transform.Find("IronMine").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.IronMine));
            transform.Find("StoneMine").GetComponent<Button>().onClick.AddListener(() => _upgrades.TryUnlock(Upgrades.ResearchUpgrades.StoneMine));           
        }

        public void SetActiveSkills(Upgrades upgrades)
        {
            this._upgrades = upgrades;
            buildui.upgrades = upgrades;
        }
    }
}
