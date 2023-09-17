using Sources.Modules.Object.Scripts.MVP;
using UnityEngine;

namespace Sources.Modules.ObjectFactory.Scripts
{
    public class ObjectComposer : MonoBehaviour
    {
        [SerializeField] private Vector3 _startSpawnPosition;

        private const float DeltaDistance = 3;
        
        public void LocateObject(ObjectView obj)
        {
            obj.transform.position = _startSpawnPosition;
            _startSpawnPosition.x += DeltaDistance;
        }
    }
}
