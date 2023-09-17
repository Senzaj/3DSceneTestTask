using Sources.Modules.Object.Scripts;
using Sources.Modules.Object.Scripts.UI;

namespace Sources.Modules.ObjectFactory.Scripts.MVP
{
    public class ObjectSpawnerPresenter
    {
        private ObjectSpawnerView _view;
        private ObjectSpawnerModel _model;

        public ObjectSpawnerPresenter(ObjectSpawnerView view, ObjectSpawnerModel model)
        {
            _view = view;
            _model = model;
        }

        public void Enable()
        {
            _view.ObjectForSpawnSelected += AddObjectForSpawn;
            _view.RemoveObjectForSpawnRequested += RemoveObjectForSpawn;
            _view.OnSpawnObjectsButtonPressed += GetObjectsData;
            _model.ObjectAdded += AddObjectPanel;
            _model.ObjectRemoved += RemoveObjectPanel;
        }

        public void Disable()
        {
            _view.ObjectForSpawnSelected -= AddObjectForSpawn;
            _view.RemoveObjectForSpawnRequested -= RemoveObjectForSpawn;
            _view.OnSpawnObjectsButtonPressed -= GetObjectsData;
            _model.ObjectAdded -= AddObjectPanel;
            _model.ObjectRemoved -= RemoveObjectPanel;
        }

        private void GetObjectsData()
        {
            _view.TrySpawnRequest(_model.GetObjects());
        }
        
        private void RemoveObjectForSpawn(ObjectShape shape, PreSpawnedObjectPanel panel)
        {
            _model.RemoveObject(shape, panel);
        }

        private void RemoveObjectPanel(PreSpawnedObjectPanel panel)
        {
            _view.RemoveObjectPanel(panel);
        }
        
        private void AddObjectPanel(ObjectShape shape)
        {
            _view.AddObjectPanel(shape);
        }
        
        private void AddObjectForSpawn(ObjectShape shape)
        {
            _model.AddObject(shape);
        }
    }
}
