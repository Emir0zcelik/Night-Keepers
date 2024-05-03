using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NightKeepers
{
    public class Barracks : MonoBehaviour
    {
        private List<Unit> _unitProductionList = new List<Unit>();

        private bool _isInProduction;
        private int _currentNumberOfProductions = 0;

        public static event Action onQueueUpdated;

        public void InsertUnitToList(Unit unitToProduce)
        {
            _unitProductionList.Add(unitToProduce);
            onQueueUpdated?.Invoke();
            _currentNumberOfProductions++;

            if (!_isInProduction)
            {
                StartCoroutine(ProduceUnit());
            }
        }

        public Unit TransferUnitFromQueueToReady()
        {
            Unit unit = _unitProductionList[0];
            _unitProductionList.RemoveAt(0);
            onQueueUpdated?.Invoke();
            _currentNumberOfProductions--;
            PlayerUnitManager.Instance.AddUnitToReadyList(unit);
            return unit;
        }

        IEnumerator ProduceUnit()
        {
            _isInProduction = true;
            while (_unitProductionList.Count > 0)
            {
                yield return new WaitForSeconds(_unitProductionList[0].UnitData.ProductionTime);
                Unit unit = TransferUnitFromQueueToReady();
                print(unit.name + " unit produced!");
            }
            _isInProduction = false;
        }

        public int GetCurrentListCount()
        {
            return _unitProductionList.Count;
        }

        public Unit GetLastElementOfList()
        {
            return _unitProductionList[0];
        }

        public int GetCurrentNumberOfProductions()
        {
            return _currentNumberOfProductions;
        }
    }
}