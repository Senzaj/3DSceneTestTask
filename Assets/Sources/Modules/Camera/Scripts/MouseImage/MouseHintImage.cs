using System;
using UnityEngine;

namespace Sources.Modules.Camera.Scripts.MouseImage
{
    public class MouseHintImage : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _thisCanvas;
        [SerializeField] private Animator _animator;
        
        private bool _wasFreeModeEnabled;
        private bool _wasInspectionModeEnabled;

        private void OnEnable()
        {
            TurnOffMouse();
        }

        public void TryTurnOnMouseForFreeMode()
        {
            if (_wasFreeModeEnabled == false)
            {
                TurnOnMouse();
                _animator.Play(MouseAnimator.States.Click);
                _wasFreeModeEnabled = true;
            }
        }

        public void TryTurnOnMouseForInspectionMode()
        {
            if (_wasInspectionModeEnabled == false)
            {
                TurnOnMouse();
                _animator.Play(MouseAnimator.States.Press);
                _wasInspectionModeEnabled = true;
            }
        }
        
        private void TurnOnMouse()
        {
            _thisCanvas.alpha = 1;
            _thisCanvas.interactable = true;
            _thisCanvas.blocksRaycasts = true;
        }
        
        private void TurnOffMouse()
        {
            _thisCanvas.alpha = 0;
            _thisCanvas.interactable = false;
            _thisCanvas.blocksRaycasts = false;
        }
    }
}
