using System;
using System.Collections.Generic;
using Sources.Modules.Object.Scripts.MVP;
using UnityEngine;

namespace Sources.Modules.ObjectList.Scripts.MVP
{
    public class ObjectListModel
    {
        private List<ObjectView> _objects;
        private List<ObjectView> _selectedObjects;

        public event Action<ObjectView> ObjectAdded;

        public ObjectListModel()
        {
            _objects = new List<ObjectView>();
            _selectedObjects = new List<ObjectView>();
        }
        
        public void AddObject(ObjectView obj)
        {
            _objects.Add(obj);
            ObjectAdded?.Invoke(obj);
        }

        public void ChangeObjectsAlpha(float alpha)
        {
            foreach (ObjectView obj in _selectedObjects)
                obj.ChangeAlphaRequest(alpha);
        }

        public void ChangeObjectsColor(Color color)
        {
            foreach (ObjectView obj in _selectedObjects)
                obj.ChangeColorRequest(color);
        }
        
        public void TrySelectObject(bool isSelected, ObjectView obj)
        {
            if (isSelected)
            {
                _selectedObjects.Add(obj);
            }
            else
            {
                if (_selectedObjects.Contains(obj))
                    _selectedObjects.Remove(obj);
            }
        }
    }
}
