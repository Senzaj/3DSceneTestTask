using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.ObjectCustomizer.Scripts
{
    public class ColorButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Color _color;

        public event Action<Color> Clicked; 

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => Clicked?.Invoke(_color);
    }
}
