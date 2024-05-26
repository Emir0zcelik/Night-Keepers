using System;
using UnityEngine;

namespace NightKeepers
{
    public class BarracksButton : MonoBehaviour
    {
        public static event Action<Unit> onButtonPressed;
        public ResourceManagement resourceManager;

        private void Start()
        {
            resourceManager = FindObjectOfType<ResourceManagement>();
        }

        public void SendPrefabToBarracks(Unit _unitToProduce)
        {
            if (resourceManager.HasEnoughResourcesForUnit(_unitToProduce.UnitData.Cost))
            {
                onButtonPressed?.Invoke(_unitToProduce);
                resourceManager.DeductResourcesForUnit(_unitToProduce.UnitData.Cost);
            }
            else
            {

                Debug.Log("Not Enough Resource");
            }
        }
    }
}
