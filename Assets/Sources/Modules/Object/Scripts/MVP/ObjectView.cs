using System;
using UnityEngine;

namespace Sources.Modules.Object.Scripts.MVP
{
    public class ObjectView : MonoBehaviour
    {
        [SerializeField] private ObjectShape _objectShape;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public ObjectShape ObjectShape => _objectShape;

        public event Action<Color> ColorChangeRequested;
        public event Action<float> AlphaChangeRequested;

        public void SetMaterial(Material material)
        {
            _meshRenderer.material = material;
        }
        
        public void ChangeColorRequest(Color color)
        {
            ColorChangeRequested?.Invoke(color);
        }

        public void ChangeAlphaRequest(float alpha)
        {
            AlphaChangeRequested?.Invoke(alpha);
        }

        public void ChangeMatColor(Color color)
        {
            _meshRenderer.material.color = color;
        }
    }
}
