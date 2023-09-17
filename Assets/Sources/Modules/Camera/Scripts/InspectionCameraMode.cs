using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace Sources.Modules.Camera.Scripts
{
    public class InspectionCameraMode : MonoBehaviour
    {
        private const float Sensitivity = 3;
        private const float Limit = 80;
        private const float ZoomSensitivity = 0.25f;
        private const float ZoomMax = 10; 
        private const float ZoomMin = 1.5f;

        private const string MouseSW = "Mouse ScrollWheel";
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        
        private Transform _target;
        private Vector3 _offset;
        private float X, Y;
        private Coroutine _inspectionWork;

        private bool _canInspect = true;

        private void OnDisable()
        {
            TryStopInspection();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            
            _offset = new Vector3(_offset.x, _offset.y, -Mathf.Abs(ZoomMax)/2);
            transform.position = _target.position + _offset;
            
            StartInspection();
        }
        
        private void StartInspection()
        {
            TryStopInspection();

            _canInspect = true;
            _inspectionWork = StartCoroutine(Inspection());
        }

        private void TryStopInspection()
        {
            if (_inspectionWork != null)
            {
                StopCoroutine(_inspectionWork);
                _canInspect = false;
            }
            
            Cursor.visible = true;
        }
        
        private IEnumerator Inspection()
        {
            Looking();
            
            while (_canInspect)
            {
                if (Input.GetMouseButton(MouseButton.RightMouse.GetHashCode()))
                {
                    Cursor.visible = false;
                    Looking();
                    Zoom();
                }
                else
                {
                    Cursor.visible = true;
                }
                
                yield return null;
            }
        }
        
        private void Zoom()
        {
            if(Input.GetAxis(MouseSW) > 0)
                _offset.z += ZoomSensitivity;
            else if(Input.GetAxis(MouseSW) < 0)
                _offset.z -= ZoomSensitivity;
            
            _offset.z = Mathf.Clamp(_offset.z, -Mathf.Abs(ZoomMax), -Mathf.Abs(ZoomMin));
        }

        private void Looking()
        {
            X = transform.localEulerAngles.y + Input.GetAxis(MouseX) * Sensitivity;
            Y += Input.GetAxis(MouseY) * Sensitivity;
            Y = Mathf.Clamp (Y, -Limit, Limit);
            transform.localEulerAngles = new Vector3(-Y, X, 0);
            transform.position = transform.localRotation * _offset + _target.position;
        }
    }
}
