using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Camera.Scripts
{
    public class FreeModeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CanvasGroup _thisCanvas;

        public event Action Clicked; 

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            TurnOff();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void TurnOn()
        {
            _thisCanvas.alpha = 1;
            _thisCanvas.interactable = true;
            _thisCanvas.blocksRaycasts = true;
        }

        private void OnClick()
        {
            TurnOff();
            Clicked?.Invoke();
        }
        
        private void TurnOff()
        {
            _thisCanvas.alpha = 0;
            _thisCanvas.interactable = false;
            _thisCanvas.blocksRaycasts = false;
        }
    }
}
