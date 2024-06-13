using UnityEngine;

namespace NightKeepers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameObjects;

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
            foreach (var gameObject in _gameObjects) { 
                gameObject.SetActive(false);
            }
        }

        private void OnDayArrived()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.SetActive(true);
            }
        }
    }
}