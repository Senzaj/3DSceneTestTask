using System;
using UnityEngine;

namespace Sources.Modules.Object.Scripts.MVP
{
    public class ObjectModel
    {
        private Color _currentColor;
        private Vector3 _currentPosition;

        public event Action<Color> ColorChanged;

        public void ChangeAlpha(float alpha)
        {
            Color newColor = new Color(_currentColor.r, _currentColor.g, _currentColor.b, alpha);
            _currentColor = newColor;
            ColorChanged?.Invoke(_currentColor);
        }

        public void ChangeColor(Color color)
        {
            Color newColor = new Color(color.r, color.g, color.b);

            _currentColor = newColor;
            ColorChanged?.Invoke(_currentColor);
        }
    }
}
