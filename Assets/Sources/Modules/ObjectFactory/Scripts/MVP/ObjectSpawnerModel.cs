using System;
using System.Collections.Generic;
using Sources.Modules.Object.Scripts;
using Sources.Modules.Object.Scripts.UI;

namespace Sources.Modules.ObjectFactory.Scripts.MVP
{
    public class ObjectSpawnerModel
    {
        public event Action<ObjectShape> ObjectAdded;
        public event Action<PreSpawnedObjectPanel> ObjectRemoved; 

        private List<ObjectShape> _objects;

        public ObjectSpawnerModel()
        {
            _objects = new List<ObjectShape>();
        }

        public List<ObjectShape> GetObjects()
        {
            return _objects;
        }
        
        public void AddObject(ObjectShape obj)
        {
            _objects.Add(obj);
            ObjectAdded?.Invoke(obj);
        }

        public void RemoveObject(ObjectShape shape, PreSpawnedObjectPanel panel)
        {
            _objects.Remove(shape);
            ObjectRemoved?.Invoke(panel);
        }
    }
}
