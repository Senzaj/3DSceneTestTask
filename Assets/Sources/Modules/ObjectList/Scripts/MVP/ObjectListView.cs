using System;
using System.Collections.Generic;
using Sources.Modules.Object.Scripts.MVP;
using Sources.Modules.Object.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.ObjectList.Scripts.MVP
{
    public class ObjectListView : MonoBehaviour
    {
        [SerializeField] private SpawnedObjectPanel _spawnedObjectPanelPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private CanvasGroup _thisCanvas;
        [SerializeField] private Toggle _toggle;

        public event Action<ObjectView> ObjectCreated;
        public event Action<bool, ObjectView> ObjectPanelChangedStatus;
        public event Action<Color> ColorChangeRequested; 
        public event Action<float> AlphaChangeRequested;
        public event Action<Transform> ObjectClicked; 

        private List<SpawnedObjectPanel> _objectPanels;

        private void OnEnable()
        {
            _objectPanels = new List<SpawnedObjectPanel>();
            _toggle.isOn = false;
            
            _toggle.onValueChanged.AddListener(OnAllPanelsChangedStatus);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnAllPanelsChangedStatus);
        }

        public void Show()
        {
            _thisCanvas.alpha = 1;
            _thisCanvas.interactable = true;
            _thisCanvas.blocksRaycasts = true;
        }

        public void Hide()
        {
            _thisCanvas.alpha = 0;
            _thisCanvas.interactable = false;
            _thisCanvas.blocksRaycasts = false;
        }
        
        public void AddObject(ObjectView obj)
        {
            ObjectCreated?.Invoke(obj);
        }

        public void LinkObjectToPanel(ObjectView obj)
        {
            SpawnedObjectPanel panel = Instantiate(_spawnedObjectPanelPrefab, _content);
            panel.SetObjectView(obj);
            panel.Init(obj.ObjectShape);
            _objectPanels.Add(panel);
            panel.ChangedStatus += OnPanelChangedStatus;
            panel.Clicked += OnPanelClicked;
        }

        public void ChangeObjectsAlpha(float alpha)
        {
            AlphaChangeRequested?.Invoke(alpha);
        }

        public void ChangeObjectsColor(Color color)
        {
            ColorChangeRequested?.Invoke(color);
        }
        
        private void OnPanelChangedStatus(bool isSelected, ObjectView obj)
        {
            ObjectPanelChangedStatus?.Invoke(isSelected, obj);
        }

        private void OnAllPanelsChangedStatus(bool isSelected)
        {
            foreach (SpawnedObjectPanel panel in _objectPanels)
                panel.SwitchToggle(isSelected);
        }

        private void OnPanelClicked(Transform objTransform)
        {
            ObjectClicked?.Invoke(objTransform);
        }
    }
}
