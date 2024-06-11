using UnityEngine;

namespace NightKeepers.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed = 25f;
        [SerializeField] private float smoothing = 6f;
        [SerializeField] private Vector2 range = new(30, 70);
        [SerializeField] private Transform cameraHolder;

        private Vector3 cameraDirection => transform.InverseTransformDirection(cameraHolder.forward);
        private Vector3 targetPosition;
        private float input;
        private float targetZoom;

        private void Awake()
        {
            targetPosition = cameraHolder.localPosition;
        }

        private void HandleControl()
        {
            input = Input.GetAxisRaw("Mouse ScrollWheel");
            if (Input.GetKey(KeyCode.R))
            {
                input = 0.1f;
            }
            else if (Input.GetKey(KeyCode.F))
            {
                input = -0.1f;
            }
            else
            {
                targetZoom = 0;
            }
            input = Mathf.Lerp(input, targetZoom, smoothing * Time.unscaledDeltaTime);
        }

        private void Zoom()
        {
            Vector3 nextPosition = targetPosition + cameraDirection * input * zoomSpeed;
            if (IsInBounds(nextPosition))   targetPosition = nextPosition;
            cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, targetPosition, smoothing * Time.unscaledDeltaTime);
        }
        private bool IsInBounds(Vector3 position)
        {
            return position.magnitude > -range.x && position.magnitude < range.y;
        }

        private void Update()
        {
            HandleControl();
            Zoom();
        }
    }
}