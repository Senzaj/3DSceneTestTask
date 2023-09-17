using Sources.Modules.ObjectFactory.Scripts.MVP;
using Sources.Modules.ObjectList.Scripts.MVP;
using UnityEngine;

namespace Sources.App
{
    internal class AppRoot : MonoBehaviour
    {
        [SerializeField] private ObjectSpawnerView _objectSpawnerView;
        [SerializeField] private ObjectListView _objectListView;

        private ObjectSpawnerPresenter _objectSpawnerPresenter;
        private ObjectListPresenter _objectListPresenter;

        private void Awake()
        {
            InitObjectSpawner();
            InitObjectList();
        }

        private void OnDisable()
        {
            _objectSpawnerPresenter.Disable();
            _objectListPresenter.Disable();
        }

        private void InitObjectSpawner()
        {
            ObjectSpawnerModel model = new ObjectSpawnerModel();
            _objectSpawnerPresenter = new ObjectSpawnerPresenter(_objectSpawnerView, model);
            _objectSpawnerPresenter.Enable();
        }

        private void InitObjectList()
        {
            ObjectListModel model = new ObjectListModel();
            _objectListPresenter = new ObjectListPresenter(_objectListView, model);
            _objectListPresenter.Enable();
            _objectListView.Hide();
        }
    }
}
