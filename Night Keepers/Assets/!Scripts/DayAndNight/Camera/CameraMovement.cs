using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace NightKeepers.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float smoothing = 5f;
        [SerializeField] private Vector2 range;
        [SerializeField] private GridManager _gridManager;
        private Vector3 startingPosition;
         
        private Vector3 targetPosition;
        private Vector3 input;
        
        private float targetAngle;
        private float angle;
       
        private void Start()
        {
            Vector3 cameraPos = new Vector3(_gridManager._grid.GetCenterOfGrid().x,_gridManager._grid.GetCenterOfGrid().x / 10, _gridManager._grid.GetCenterOfGrid().z);
            startingPosition = cameraPos;
            transform.position = startingPosition;
            targetPosition = transform.position;
            targetAngle = transform.eulerAngles.y;
            angle = targetAngle;
            range = _gridManager._grid.GetXZRanges();
            
        }

        private void HandleControl()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 right = transform.right * x;
            Vector3 forward = transform.forward * z;

            input = (right + forward).normalized;

            
            if (Input.GetKey(KeyCode.Q))
            {
                targetAngle -= speed * 4; 
            }

            
            if (Input.GetKey(KeyCode.E))
            {
                targetAngle += speed * 4; 
            }

            // Existing mouse rotation code
            if (Input.GetMouseButton(1))
            {
                targetAngle += Input.GetAxisRaw("Mouse X") * speed * 4;
            }
        }

        private void Move()
        {
            Vector3 nextPosition = targetPosition + input * speed / 5;
            if (IsInBounds(nextPosition)) targetPosition = nextPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
        private void Rotation()
        {
            angle = Mathf.Lerp(angle, targetAngle, smoothing * Time.deltaTime *3);
            transform.rotation = Quaternion.AngleAxis(angle,Vector3.up);
        }
        
        
        private bool IsInBounds(Vector3 position)
        {
            return position.x > startingPosition.x - range.x && 
                position.x < startingPosition.x + range.x &&
                position.z < startingPosition.z + range.y && 
                position.z > startingPosition.z -range.y;
        }
       
        private void Update()
        {
            HandleControl();
            Move();
            Rotation(); 
        }
    }
}