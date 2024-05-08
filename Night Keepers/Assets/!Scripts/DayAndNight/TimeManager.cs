using System;
using UnityEngine;

namespace NightKeepers
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField] private float dayTimeLenght;
        [SerializeField] private float nightTimeLenght;

        private float timeLenght;

        private float _globalTime;
        private bool _isDay = true;

        public static event Action OnNightArrived;

        public float GlobalTime 
        {
            get { return _globalTime; }
        }

        public bool IsDay 
        {
            get { return _isDay; }
        }

        private void Start()
        {
            timeLenght = dayTimeLenght;
        }

        void Update()
        {
            _globalTime += Time.deltaTime;

            if (_globalTime >= timeLenght)
            {

                _globalTime = 0f;
                _isDay = !_isDay;

                if (!_isDay)
                {
                    timeLenght = nightTimeLenght;
                    OnNightArrived?.Invoke();
                    Debug.Log("Day Time Ended.");
                }
                else
                {
                    timeLenght = dayTimeLenght;
                    Debug.Log("Night Time Ended.");
                }
            }
        }
    }
}