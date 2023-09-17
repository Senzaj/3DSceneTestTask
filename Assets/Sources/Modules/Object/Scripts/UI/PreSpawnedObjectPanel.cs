using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Object.Scripts.UI
{
    public class PreSpawnedObjectPanel : ObjectPanel
    {
        [SerializeField] private Button _removeButton;

        public event Action<PreSpawnedObjectPanel> RemovedButtonPressed;

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(RemoveButtonPressed);
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(RemoveButtonPressed);
        }

        private void RemoveButtonPressed()
        {
            RemovedButtonPressed?.Invoke(this);
        }
    }
}
