using MorseGame.Object.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object.Manager
{
    public class ObjectManager : MonoBehaviour
    {
        private List<ObjectBase> _Objects = new List<ObjectBase>();
        [SerializeField] private bool _DebugSendMorse = false;
        [SerializeField] private List<MorseData> _DebugSendMorseData = new List<MorseData>();

        private ObjectBase _ShowUIObject = null;

        private void Update()
        {
            foreach(ObjectBase obj in _Objects)
            {
                obj.LogicUpdate();
            }

            if (_DebugSendMorse)
            {
                _DebugSendMorse = false;
                ReceiveMorseInput(_DebugSendMorseData);
            }
        }

        public void AddObject(ObjectBase obj)
        {
            if(!_Objects.Contains(obj))
            {
                _Objects.Add(obj);
            }            
        }

        public void ReceiveMorseInput(List<MorseData> inputMorseData)
        {
            foreach(ObjectBase obj in _Objects)
            {
                List<MorseData> checkData = obj.GetMorseData().MorseData;
                if (inputMorseData.Count != checkData.Count) continue;

                bool isSame = true;
                for(int i = 0; i<checkData.Count;i++)
                {
                    if (inputMorseData[i].MorseNumber != checkData[i].MorseNumber)
                    {
                        isSame = false;
                        break;
                    }
                }

                if(isSame)
                {
                    obj.ReceiveInteract();
                    //break;
                }
            }
        }

        public void ReceiveShowMorseUI(ObjectBase obj)
        {
            foreach(ObjectBase current in _Objects)
            {
                if (current != obj) continue;
                _ShowUIObject = current;
                current.ShowMorseUI();
                break;
            }
        }

        public void ReceiveHideMorseUI()
        {
            if (_ShowUIObject == null) return;
            _ShowUIObject.HideMorseUI();
            _ShowUIObject = null;
        }
    }
}
