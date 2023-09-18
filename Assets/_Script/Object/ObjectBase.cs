using MorseGame.Object.Data;
using MorseGame.Object.Manager;
using MorseGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectBase : MonoBehaviour
    {
        [SerializeField] private ObjectMorseData _MorseData;
        [SerializeField] private PopUPWindow _PopUpWindow;
        [SerializeField] protected bool _isDebugInteract = false;
        [SerializeField] private float _TextSize = 1.5f;
        [SerializeField] private float _TextHeight = 1.0f;
        protected bool NowState = false;

        protected virtual void Start()
        {
            if(transform.parent.TryGetComponent<ObjectManager>(out ObjectManager manager))
            {
                manager.AddObject(this);
            }
            else
            {
                Debug.LogError("ObjectManagerÇ™" + transform.parent.name + "Ç…å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅB");
            }

            Vector2 popUpSize = new Vector2(_MorseData.MorseData.Count * _TextSize, _TextHeight);
            _PopUpWindow.SetSize(popUpSize);
            string text = "";
            for (int i = 0; i < _MorseData.MorseData.Count; i++)
            {
                if (_MorseData.MorseData[i].MorseNumber == 0) text += "ÅE";
                else text += "Å[";
            }
            _PopUpWindow.SetText(text);
            HideMorseUI();
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

        public void ShowMorseUI()
        {
            _PopUpWindow.gameObject.SetActive(true);
        }

        public void HideMorseUI()
        {
            _PopUpWindow.gameObject.SetActive(false);
        }
    }
}
