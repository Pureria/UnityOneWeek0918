using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object.Manager
{
    public class ObjectManager : MonoBehaviour
    {
        private List<ObjectBase> _Objects = new List<ObjectBase>();

        private void Update()
        {
            foreach(ObjectBase obj in _Objects)
            {
                obj.LogicUpdate();
            }
        }

        public void AddObject(ObjectBase obj)
        {
            if(!_Objects.Contains(obj))
            {
                _Objects.Add(obj);
            }
        }
    }
}
