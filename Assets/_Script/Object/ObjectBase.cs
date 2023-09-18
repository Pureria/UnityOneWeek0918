using MorseGame.Object.Data;
using MorseGame.Object.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectBase : MonoBehaviour
    {
        [SerializeField] private ObjectMorseData _MorseData;
        [SerializeField] protected bool _isDebugInteract = false;
        protected bool NowState = false;

        protected virtual void Start()
        {
            if(transform.parent.TryGetComponent<ObjectManager>(out ObjectManager manager))
            {
                manager.AddObject(this);
            }
            else
            {
                Debug.LogError("ObjectManager‚ª" + transform.parent.name + "‚ÉŒ©‚Â‚©‚è‚Ü‚¹‚ñB");
            }
        }

        public virtual void ReceiveInteract() { NowState = !NowState; }

        public virtual void LogicUpdate() 
        {
            if (_isDebugInteract)
            {
                _isDebugInteract = false;
                ReceiveInteract();
            }
        }

        protected float Animation(float startTime, float endTime, float startKey, float endKey, float nowTime)
        {
            float sKey = startKey;
            float ekey = endKey;
            bool check = false;

            if(startKey > endKey)
            {
                sKey = endKey;
                ekey = startKey;
                check = true;
            }

            float ret = 0.0f;
            float t = endTime - startTime;
            float k = ekey - sKey;
            t = (nowTime - startTime) / t;
            ret = (k * t) + sKey;
            ret = Mathf.Clamp(ret, sKey, ekey);

            if(check)
            {
                ret = (startKey - endKey) - ret;
            }

            return ret;
        }

        public ObjectMorseData GetMorseData() { return _MorseData; }
    }
}
