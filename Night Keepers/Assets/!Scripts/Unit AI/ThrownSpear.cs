using UnityEngine;

namespace NightKeepers
{
    public class ThrownSpear : MonoBehaviour
    {
        public float speed = 10f;
        public float arcHeight = 5f;

        private Vector3 _targetPosition;
        private Vector3 startPosition;
        private float journeyLength;
        private float startTime;

        private bool _isThrown = false;

        void Update()
        {
            if (!_isThrown) return;

            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;

            Vector3 currentPosition = Vector3.Lerp(startPosition, _targetPosition, fractionOfJourney);

            currentPosition.y += arcHeight * Mathf.Sin(Mathf.Clamp01(fractionOfJourney) * Mathf.PI);

            transform.position = currentPosition;
            Vector3 direction = (_targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);
            transform.LookAt(_targetPosition);
        }

        public void Thrown(Vector3 startingPos, Vector3 targetPos)
        {
            startPosition = startingPos;
            _targetPosition = targetPos;
            journeyLength = Vector3.Distance(startPosition, _targetPosition);
            _isThrown = true;
            startTime = Time.time;
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}