using Sources.Modules.Object.Scripts;
using Sources.Modules.Object.Scripts.MVP;
using UnityEngine;

namespace Sources.Modules.ObjectFactory.Scripts
{
    public class ObjectFactory : MonoBehaviour
    {
        [SerializeField] private ObjectView[] _objectPrefabs;
        [SerializeField] private Transform _objectContainer;
        [SerializeField] private ObjectComposer _objectComposer;
        [SerializeField] private Material _material;

        public ObjectView CreateObject(ObjectShape shape)
        {
            ObjectView spawnedObj = null;
            
            foreach (ObjectView prefab in _objectPrefabs)
            {
                if (prefab.ObjectShape == shape)
                {
                    ObjectView newObject = Instantiate(prefab, _objectContainer);
                    
                    newObject.SetMaterial(Instantiate(_material));

                    ObjectModel model = new ObjectModel();
                    ObjectPresenter presenter = new ObjectPresenter(newObject, model);
                    presenter.Enable();
                    
                    _objectComposer.LocateObject(newObject);
                    
                    spawnedObj = newObject;
                }
            }

            return spawnedObj;
        }
    }
}
