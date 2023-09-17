using Sources.Modules.ObjectList.Scripts.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.ObjectCustomizer.Scripts
{
    public class ObjectCustomizer : MonoBehaviour
    {
        [SerializeField] private Slider _alphaSlider;
        [SerializeField] private Button _alphaButton;
        [SerializeField] private ColorButton[] _colorButtons;
        [SerializeField] private ObjectListView _objectList;
        
        private const float MinValue = 0;
        private const float MaxValue = 1;
        
        private void OnEnable()
        {
            _alphaSlider.onValueChanged.AddListener(ChangeAlpha);
            _alphaButton.onClick.AddListener(OnAlphaButtonPressed);

            foreach (ColorButton button in _colorButtons)
                button.Clicked += OnColorButtonPressed;
        }

        private void OnDisable()
        {
            _alphaSlider.onValueChanged.RemoveListener(ChangeAlpha);
            _alphaButton.onClick.RemoveListener(OnAlphaButtonPressed);

            foreach (ColorButton button in _colorButtons)
                button.Clicked -= OnColorButtonPressed;
        }

        private void OnColorButtonPressed(Color color)
        {
            _objectList.ChangeObjectsColor(color);
        }
        
        private void OnAlphaButtonPressed()
        {
            if (_alphaSlider.value == MinValue)
                SetAlphaValueByButton(MaxValue);
            else
                SetAlphaValueByButton(MinValue);
        }
        
        private void SetAlphaValueByButton(float alpha)
        {
            _alphaSlider.value = alpha;
            ChangeAlpha(alpha);
        }
        
        private void ChangeAlpha(float alpha)
        {
            _objectList.ChangeObjectsAlpha(alpha);
        }
    }
}
