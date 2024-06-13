using UnityEngine;

namespace NightKeepers
{
    public class ChangeBuildingLights : MonoBehaviour
    {
        [SerializeField] private GameObject[] lights;

        private void OnEnable()
        {
            TimeManager.OnNightArrived += OnNightArrived;
            TimeManager.OnDayArrived += OnDayArrived;
        }

        private void OnDisable()
        {
            TimeManager.OnNightArrived -= OnNightArrived;
            TimeManager.OnDayArrived -= OnDayArrived;
        }

        private void OnNightArrived()
        {
            foreach (GameObject light in lights) {
                light.SetActive(true);
            }
        }

        private void OnDayArrived()
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
        }
    }
}