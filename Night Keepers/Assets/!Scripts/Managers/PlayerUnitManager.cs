using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NightKeepers
{
    public class PlayerUnitManager : Singleton<PlayerUnitManager>
    {
        [SerializeField] private List<Unit> _readyUnitList = new List<Unit>();

        [SerializeField] private RectTransform _readyUnitHolder;

        public void AddUnitToReadyList(Unit unit)
        {
            _readyUnitList.Add(unit);

            GameObject newImageObj = new GameObject(unit.name);

            Image newImage = newImageObj.AddComponent<Image>();
            // temporary
            if (unit.UnitData.UnitName == "Green")
            {
                newImage.color = Color.green;
            }
            else if (unit.UnitData.UnitName == "Purple")
            {
                newImage.color = Color.magenta;
            }
            newImageObj.transform.SetParent(_readyUnitHolder.transform, false);
        }
    }
}