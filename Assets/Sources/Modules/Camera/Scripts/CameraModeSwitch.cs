using Sources.Modules.Camera.Scripts.MouseImage;
using Sources.Modules.ObjectFactory.Scripts.MVP;
using Sources.Modules.ObjectList.Scripts.MVP;
using UnityEngine;

namespace Sources.Modules.Camera.Scripts
{
    [RequireComponent(typeof(InspectionCameraMode))]
    [RequireComponent(typeof(FreeCameraMode))]
    public class CameraModeSwitch : MonoBehaviour
    {
        [SerializeField] private ObjectSpawnerView _objectSpawner;
        [SerializeField] private ObjectListView _objectList;
        [SerializeField] private MouseHintImage _mouse;
        [SerializeField] private FreeModeButton _freeModeButton;

        private InspectionCameraMode _inspectionMode;
        private FreeCameraMode _freeMode;

        private void OnEnable()
        {
            _inspectionMode = GetComponent<InspectionCameraMode>();
            _freeMode = GetComponent<FreeCameraMode>();

            _objectSpawner.ObjectsSpawned += EnableFreeMode;
            _freeModeButton.Clicked += EnableFreeMode;
            _objectList.ObjectClicked += EnableInspectionMode;
        }

        private void OnDisable()
        {
            _objectSpawner.ObjectsSpawned -= EnableFreeMode;
            _freeModeButton.Clicked -= EnableFreeMode;
            _objectList.ObjectClicked -= EnableInspectionMode;
        }

        private void EnableFreeMode()
        {
            _mouse.TryTurnOnMouseForFreeMode();
            
            if (_inspectionMode.enabled)
                _inspectionMode.enabled = false;
            
            _freeMode.enabled = true;
        }

        private void EnableInspectionMode(Transform objTransform)
        {
            _mouse.TryTurnOnMouseForInspectionMode();
            
            if (_freeMode.enabled)
                _freeMode.enabled = false;
            
            _inspectionMode.enabled = true;
            _inspectionMode.SetTarget(objTransform);
            
            _freeModeButton.TurnOn();
        }
    }
}
