using Sources.Modules.Object.Scripts.MVP;
using UnityEngine;

namespace Sources.Modules.ObjectList.Scripts.MVP
{
    public class ObjectListPresenter
    {
        private ObjectListView _view;
        private ObjectListModel _model;

        public ObjectListPresenter(ObjectListView view, ObjectListModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.ObjectCreated += OnObjectCreated;
            _view.ObjectPanelChangedStatus += OnObjectPanelChangedStatus;
            _view.AlphaChangeRequested += OnAlphaChangeRequest;
            _view.ColorChangeRequested += OnColorChangeRequest;
            _model.ObjectAdded += AddObjectPanel;
        }

        public void Disable()
        {
            _view.ObjectCreated -= OnObjectCreated;
            _view.ObjectPanelChangedStatus -= OnObjectPanelChangedStatus;
            _view.AlphaChangeRequested -= OnAlphaChangeRequest;
            _view.ColorChangeRequested -= OnColorChangeRequest;
            _model.ObjectAdded -= AddObjectPanel;
        }

        private void OnAlphaChangeRequest(float alpha)
        {
            _model.ChangeObjectsAlpha(alpha);
        }

        private void OnColorChangeRequest(Color color)
        {
            _model.ChangeObjectsColor(color);
        }
        
        private void OnObjectCreated(ObjectView obj)
        {
            _model.AddObject(obj);
        }

        private void AddObjectPanel(ObjectView obj)
        {
            _view.LinkObjectToPanel(obj);
        }

        private void OnObjectPanelChangedStatus(bool isSelected, ObjectView obj)
        {
            _model.TrySelectObject(isSelected, obj);
        }
    }
}
