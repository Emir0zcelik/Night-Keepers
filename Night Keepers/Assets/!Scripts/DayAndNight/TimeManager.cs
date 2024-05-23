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
        public static event Action OnDayArrived;

        public bool isTimeStarted = false;

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
            if (isTimeStarted)
            {
                _globalTime += Time.deltaTime;

                if (_globalTime >= timeLenght)
                {

                    _globalTime = 0f;
                    _isDay = !_isDay;

                    if (!_isDay)
                    {
                        Debug.Log("Night Time.");
                        timeLenght = nightTimeLenght;
                        OnNightArrived?.Invoke();
                    }
                    else
                    {
                        Debug.Log("Day Time.");
                        timeLenght = dayTimeLenght;
                        OnDayArrived?.Invoke();
                    }
                }      
            }
        }
    }
}