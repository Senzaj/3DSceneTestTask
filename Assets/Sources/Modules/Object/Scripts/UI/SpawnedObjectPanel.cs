using System;
using Sources.Modules.Object.Scripts.MVP;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Object.Scripts.UI
{
    public class SpawnedObjectPanel : ObjectPanel
    {
        [SerializeField] private Toggle _selectToggle;
        [SerializeField] private Button _button;

        public ObjectView ObjView { get; private set; }

        public event Action<bool, ObjectView> ChangedStatus;
        public event Action<Transform> Clicked; 

        private bool _isSelected;

        private void OnEnable()
        {
            _selectToggle.isOn = false;
            _selectToggle.onValueChanged.AddListener(TryChangeStatus);
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _selectToggle.onValueChanged.RemoveListener(TryChangeStatus);
            _button.onClick.RemoveListener(OnClick);
        }

        public void SwitchToggle(bool isSelected)
        {
            _selectToggle.isOn = isSelected;
        }
        
        public void SetObjectView(ObjectView objectView)
        {
            ObjView = objectView;
        }

        private void TryChangeStatus(bool isSelected)
        {
            _isSelected = isSelected;
            ChangedStatus?.Invoke(_isSelected, ObjView);
        }

        private void OnClick()
        {
            Clicked?.Invoke(ObjView.transform);
        }
    }
}
