using UnityEngine;

namespace Sources.Modules.Object.Scripts.MVP
{
    public class ObjectPresenter
    {
        private ObjectView _view;
        private ObjectModel _model;

        public ObjectPresenter(ObjectView view, ObjectModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.ColorChangeRequested += OnColorChangeRequested;
            _view.AlphaChangeRequested += OnAlphaChangeRequested;
            _model.ColorChanged += ChangeViewColor;
        }

        public void Disable()
        {
            _view.ColorChangeRequested -= OnColorChangeRequested;
            _view.AlphaChangeRequested -= OnAlphaChangeRequested;
            _model.ColorChanged -= ChangeViewColor;
        }
        
        private void OnColorChangeRequested(Color color) => _model.ChangeColor(color);

        private void OnAlphaChangeRequested(float alpha) => _model.ChangeAlpha(alpha); 

        private void ChangeViewColor(Color color) => _view.ChangeMatColor(color);

    }
}
