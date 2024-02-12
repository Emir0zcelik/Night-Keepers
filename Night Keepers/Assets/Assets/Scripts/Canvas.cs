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
            MeleeUnits,
            RangeUnits,
            Buildings,
            Others
        }
       
        //Do list of CanvasButtons
        
        void Start()
        {
            //text.text will increase every 2 seconds by 1-4.
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
        }
        public Upgrades GetUpgrades()
        {
            return _upgrades;
        }

        public bool MeleeUnitsBuffActive()
        {
            return _upgrades.IsUnlocked(Upgrades.ResearchUpgrades.MeleeUnitsBuff);
        }


    }
}
