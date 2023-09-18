using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectBridge : ObjectBase
    {
        [SerializeField,Tooltip("�Q�[���J�n���ɋ����˂����Ă��邩�H TRUE�F�˂����Ă���@FALSE : �˂����Ă��Ȃ�")] private bool InitBridge = false;
        [SerializeField] private float _BridgeChangeTime;

        [SerializeField] private Transform _LBridgeTran;
        [SerializeField] private Transform _RBridgeTran;        

        private bool nowChange;
        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitBridge;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(nowChange)
            {
                float startKeyL = 0;
                float endKeyL = 0;
                float startKeyR = 0.0f;
                float endKeyR = 0;
                if (!NowState)
                {
                    startKeyL = 0;
                    endKeyL = 90.0f;
                    startKeyR = 0;
                    endKeyR = 90.0f;
                }
                else
                {
                    startKeyL = 90.0f;
                    endKeyL = 0.0f;
                    startKeyR = 90.0f;
                    endKeyR = 0.0f;
                }

                float rot = Animation(ChangeStartTime, ChangeStartTime + _BridgeChangeTime, startKeyL, endKeyL, Time.time);
                Vector3 euler = _LBridgeTran.eulerAngles;
                euler.z = rot;
                _LBridgeTran.eulerAngles = euler;

                rot = Animation(ChangeStartTime, ChangeStartTime + _BridgeChangeTime, startKeyR, endKeyR, Time.time);
                euler = _RBridgeTran.eulerAngles;
                euler.z = rot * -1.0f;
                _RBridgeTran.eulerAngles = euler;
            
                if(ChangeStartTime + _BridgeChangeTime < Time.time)
                {
                    nowChange = false;
                    euler = _LBridgeTran.eulerAngles;
                    euler.z = endKeyL;
                    _LBridgeTran.eulerAngles = euler;
                    euler = _RBridgeTran.eulerAngles;
                    euler.z = endKeyR * -1.0f;
                    _RBridgeTran.eulerAngles = euler;
                }
            }
        }

        public override void ReceiveInteract()
        {
            //���݋��̏�Ԃ�ύX���Ȃ�󂯎��Ȃ�
            if (nowChange) return;

            base.ReceiveInteract();
            nowChange = true;
            ChangeStartTime = Time.time;
        }
    }
}
