using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NightKeepers
{
    public class PlayerUnitManager : Singleton<PlayerUnitManager>
    {
        private Dictionary<Unit, int> _unitCounts = new Dictionary<Unit, int>();

        [SerializeField] private RectTransform _readyUnitHolder;
        [SerializeField] private List<GameObject> _unitButtons;

        private void Start()
        {
            foreach (Transform unitButton in _readyUnitHolder)
            {
                _unitButtons.Add(unitButton.gameObject);
                unitButton.gameObject.SetActive(false);
            }
        }

        public void AddUnitToReadyList(Unit unit)
        {
            if (_unitCounts.ContainsKey(unit))
            {
                _unitCounts[unit]++;
            }
            else
            {
                _unitCounts[unit] = 1;
            }

            UpdateButtons(unit);
        }

        private void UpdateButtons(Unit unit)
        {
            foreach (GameObject unitButton in _unitButtons)
            {
                var selectUnitButton = unitButton.GetComponent<SelectUnitButton>();
                if (unit.UnitData.UnitButtonPrefab.name == unitButton.name)
                {
                    unitButton.SetActive(true);
                    selectUnitButton.UpdateText(GetUnitCount(unit));
                }
            }
        }

        public int GetUnitCount(Unit unit)
        {
            return _unitCounts.ContainsKey(unit) ? _unitCounts[unit] : 0;
        }

        public Unit GetUnitByButtonName(string buttonName)
        {
            Unit unit = _unitCounts.FirstOrDefault(kvp => kvp.Key.UnitData.UnitButtonPrefab.name == buttonName && kvp.Value != 0).Key;
            return unit;
        }

        public void DecreaseUnitCount(Unit unit)
        {
            if (_unitCounts.ContainsKey(unit))
            {
                _unitCounts[unit]--;
                UpdateButtons(unit);

                //if (_unitCounts[unit] <= 0)
                //{
                //    _unitCounts.Remove(unit);
                //}
            }
        }
    }
}