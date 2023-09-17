using System;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Object.Scripts.UI
{
    public class ObjectPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;

        private const string Cube = "Cube";
        private const string Sphere = "Sphere";
        private const string Cylinder = "Cylinder";
        
        public ObjectShape Shape { get; private set; }

        public void Init(ObjectShape shape)
        {
            Shape = shape;

            _title.text = shape switch
            {
                ObjectShape.Cube => Cube,
                ObjectShape.Sphere => Sphere,
                ObjectShape.Cylinder => Cylinder,
                _ => _title.text
            };
        }
    }
}
