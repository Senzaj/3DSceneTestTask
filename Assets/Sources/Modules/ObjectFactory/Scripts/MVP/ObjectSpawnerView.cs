using System;
using System.Collections.Generic;
using Sources.Modules.Object.Scripts;
using Sources.Modules.Object.Scripts.UI;
using Sources.Modules.ObjectList.Scripts.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.ObjectFactory.Scripts.MVP
{
    public class ObjectSpawnerView : MonoBehaviour
    {
        [SerializeField] private PreSpawnedObjectPanel _objectPanelPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private Button _ChooseObjectShapeButton;
        [SerializeField] private Button _spawnCubeButton;
        [SerializeField] private Button _spawnSphereButton;
        [SerializeField] private Button _spawnCylinderButton;
        [SerializeField] private Button _spawnObjectsButton;
        [SerializeField] private CanvasGroup _thisCanvas;
        [SerializeField] private CanvasGroup _submenuCanvas;
        [SerializeField] private ObjectFactory _objectFactory;
        [SerializeField] private ObjectListView _objectList;

        public event Action<ObjectShape> ObjectForSpawnSelected;
        public event Action<ObjectShape, PreSpawnedObjectPanel> RemoveObjectForSpawnRequested;
        public event Action OnSpawnObjectsButtonPressed;
        public event Action ObjectsSpawned;

        private List<PreSpawnedObjectPanel> _objectPanels;

        private void OnEnable()
        {
            _objectPanels = new List<PreSpawnedObjectPanel>();
            HideCanvas(_submenuCanvas);
            
            _ChooseObjectShapeButton.onClick.AddListener(ShowSubmenu);
            _spawnCubeButton.onClick.AddListener(SelectCubeForSpawn);
            _spawnSphereButton.onClick.AddListener(SelectSphereForSpawn);
            _spawnCylinderButton.onClick.AddListener(SelectCylinderForSpawn);
            _spawnObjectsButton.onClick.AddListener(SpawnObjectsButtonPressed);
        }
        
        private void OnDisable()
        {
            _ChooseObjectShapeButton.onClick.RemoveListener(ShowSubmenu);
            _spawnCubeButton.onClick.RemoveListener(SelectCubeForSpawn);
            _spawnSphereButton.onClick.RemoveListener(SelectSphereForSpawn);
            _spawnCylinderButton.onClick.RemoveListener(SelectCylinderForSpawn);
            _spawnObjectsButton.onClick.RemoveListener(SpawnObjectsButtonPressed);
        }

        public void TrySpawnRequest(List<ObjectShape> objects)
        {
            if (objects.Count > 0)
            {
                foreach (ObjectShape objectShape in objects)
                    _objectList.AddObject(_objectFactory.CreateObject(objectShape));

                HideCanvas(_thisCanvas);
                HideCanvas(_submenuCanvas);
                _objectList.Show();
                
                ObjectsSpawned?.Invoke();
            }
        }

        public void AddObjectPanel(ObjectShape shape)
        {
            PreSpawnedObjectPanel newObjectPanel = Instantiate(_objectPanelPrefab, _content);
            newObjectPanel.Init(shape);
            _objectPanels.Add(newObjectPanel);
            newObjectPanel.RemovedButtonPressed += OnRemoveObjectPanelButtonPressed;
        }

        public void RemoveObjectPanel(PreSpawnedObjectPanel panel)
        {
            panel.RemovedButtonPressed -= OnRemoveObjectPanelButtonPressed;
            _objectPanels.Remove(panel);
            Destroy(panel.gameObject);
        }

        private void SelectCubeForSpawn()
        {
            ObjectForSpawnSelected?.Invoke(ObjectShape.Cube);
            HideCanvas(_submenuCanvas);
        }

        private void SelectSphereForSpawn()
        {
            ObjectForSpawnSelected?.Invoke(ObjectShape.Sphere);
            HideCanvas(_submenuCanvas);
        }

        private void SelectCylinderForSpawn()
        {
            ObjectForSpawnSelected?.Invoke(ObjectShape.Cylinder);
            HideCanvas(_submenuCanvas);
        }

        private void OnRemoveObjectPanelButtonPressed(PreSpawnedObjectPanel objectPanel)
        {
            RemoveObjectForSpawnRequested?.Invoke(objectPanel.Shape, objectPanel);
        }

        private void SpawnObjectsButtonPressed()
        {
            OnSpawnObjectsButtonPressed?.Invoke();
        }

        private void ShowSubmenu() => ShowCanvas(_submenuCanvas);

        private void ShowCanvas(CanvasGroup canvas)
        {
            canvas.alpha = 1;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }

        private void HideCanvas(CanvasGroup canvas)
        {
            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
    }
}
