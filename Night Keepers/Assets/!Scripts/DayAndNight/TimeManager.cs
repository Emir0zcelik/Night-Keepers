using System;
using TMPro;
using UnityEngine;

namespace NightKeepers
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField] private float dayTimeLength;
        [SerializeField] private float nightTimeLength;
        [SerializeField] private TextMeshProUGUI _dayCountText;

        private float timeLength;
        private float _globalTime;
        private bool _isDay = true;
        private int _dayCount = 1;

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

        public int DayCount
        {
            get { return _dayCount; }
        }

        private void Start()
        {
            timeLength = dayTimeLength;
        }

        void Update()
        {
            if (isTimeStarted)
            {
                _globalTime += Time.deltaTime;

                
                if (_globalTime >= timeLength)
                {
                    _globalTime = 0f;
                    _isDay = !_isDay;

                    if (!_isDay)
                    {
                        timeLength = nightTimeLength;
                        OnNightArrived?.Invoke();
                    }
                    else
                    {
                        timeLength = dayTimeLength;
                        OnDayArrived?.Invoke();
                        _dayCount++;
                        _dayCountText.text = "Day: " + DayCount;
                    }
                }
            }
        }

        public float GetProgressionRatio()
        {
            return _globalTime / timeLength;
        }
    }
}