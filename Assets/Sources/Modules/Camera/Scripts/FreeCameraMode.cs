using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace Sources.Modules.Camera.Scripts
{
    public class FreeCameraMode : MonoBehaviour
    {
        private enum RotationAxes { MouseXAndY = 0, MouseX = 1}
        private readonly RotationAxes Axes = RotationAxes.MouseXAndY;
        
        private const float MainSpeed = 10;
        private const float SensitivityX = 4;
        private const float SensitivityY = 4;
        private const float MinimumY = -60;
        private const float MaximumY = 60;
        
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        
        private float _rotationY;
        private float _totalRun = 1;
        private Coroutine _movementWork;
        private bool _canMove;

        private void OnEnable()
        {
            _canMove = false;
            TryStopMovement();
        }

        private void OnDisable()
        {
            TryStopMovement();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(MouseButton.RightMouse.GetHashCode()))
            {
                if (_canMove)
                {
                    _canMove = false;
                    TryStopMovement();
                }
                else
                {
                    _canMove = true;
                    StartMovement();
                }
            }
        }

        private void StartMovement()
        {
            TryStopMovement();

            _movementWork = StartCoroutine(Movement());
        }

        private void TryStopMovement()
        {
            if (_movementWork != null)
                StopCoroutine(_movementWork);

            Cursor.visible = true;
        }
        
        private IEnumerator Movement()
        {
            while (_canMove)
            {
                Cursor.visible = false;
                MouseLooking();
                KeyboardMovement();
                
                yield return null;
            }
            
            Cursor.visible = true;
        }

        private void MouseLooking()
        {
            if (Axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis(MouseX) * SensitivityX;
			
                _rotationY += Input.GetAxis(MouseY) * SensitivityY;
                _rotationY = Mathf.Clamp (_rotationY, MinimumY, MaximumY);
			
                transform.localEulerAngles = new Vector3(-_rotationY, rotationX, 0);
            }
            else if (Axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis(MouseX) * SensitivityX, 0);
            }
            else
            {
                _rotationY += Input.GetAxis(MouseY) * SensitivityY;
                _rotationY = Mathf.Clamp (_rotationY, MinimumY, MaximumY);
			
                transform.localEulerAngles = new Vector3(-_rotationY, transform.localEulerAngles.y, 0);
            }
        }

        private void KeyboardMovement()
        {
            Vector3 direction = GetBaseInput();
            _totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
            direction *= MainSpeed * Time.deltaTime;
            transform.Translate(direction);
        }
        
        private Vector3 GetBaseInput()
        {
            Vector3 p_Velocity = new Vector3();

            if (Input.GetKey(KeyCode.W))
                p_Velocity += new Vector3(0, 0, 1);

            if (Input.GetKey(KeyCode.S))
                p_Velocity += new Vector3(0, 0, -1);

            if (Input.GetKey(KeyCode.A))
                p_Velocity += new Vector3(-1, 0, 0);

            if (Input.GetKey(KeyCode.D))
                p_Velocity += new Vector3(1, 0, 0);

            return p_Velocity;
        }
    }
}
