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
        [SerializeField] private Button _hideButton;
        [SerializeField] private Button _showButton;
        [SerializeField] private CanvasGroup _showButtonCanvas;

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
            
            HideCanvas(_showButtonCanvas);
            
            _toggle.onValueChanged.AddListener(OnAllPanelsChangedStatus);
            _hideButton.onClick.AddListener(OnHideButtonClicked);
            _showButton.onClick.AddListener(OnShowButtonClicked);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnAllPanelsChangedStatus);
            _hideButton.onClick.RemoveListener(OnHideButtonClicked);
            _showButton.onClick.RemoveListener(OnShowButtonClicked);
        }

        public void Show()
        {
            ShowCanvas(_thisCanvas);
        }

        public void Hide()
        {
            HideCanvas(_thisCanvas);
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

        private void OnHideButtonClicked()
        {
            Hide();
            ShowCanvas(_showButtonCanvas);
        }

        private void OnShowButtonClicked()
        {
            Show();
            HideCanvas(_showButtonCanvas);
        }

        private void ShowCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        private void HideCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
