using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class PlayerUnitManager : Singleton<PlayerUnitManager>
    {
        [SerializeField] private List<Unit> _readyUnitList = new List<Unit>();

        public void AddUnitToReadyList(Unit unit)
        {
            _readyUnitList.Add(unit);
        }
    }
}