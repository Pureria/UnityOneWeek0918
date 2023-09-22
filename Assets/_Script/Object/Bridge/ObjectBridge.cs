using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

namespace MorseGame.Object
{
    public class ObjectBridge : ObjectBase
    {
        [SerializeField,Tooltip("ゲーム開始時に橋が架かっているか？ TRUE：架かっている　FALSE : 架かっていない")] private bool InitBridge = false;
        [SerializeField] private float _BridgeChangeTime;

        [SerializeField] private Transform _LBridgeTran;
        [SerializeField] private Transform _RBridgeTran;

        [SerializeField] private Collider2D _LBridgeCollider;
        [SerializeField] private Collider2D _RBridgeCollider;
        [SerializeField] private Collider2D _LBridgeStopCollider;
        [SerializeField] private Collider2D _RBridgeStopCollider;

        private bool nowChange;
        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitBridge;
            bool setCollider = false;
            if (!NowState) setCollider = true;
            _LBridgeStopCollider.enabled = setCollider;
            _RBridgeStopCollider.enabled = setCollider;
            _LBridgeCollider.enabled = !setCollider;
            _RBridgeCollider.enabled = !setCollider;

            if(NowState)
            {
                _LBridgeTran.eulerAngles = new Vector3(_LBridgeTran.eulerAngles.x, _LBridgeTran.eulerAngles.y, 0);
                _RBridgeTran.eulerAngles = new Vector3(_RBridgeTran.eulerAngles.x, _RBridgeTran.eulerAngles.y, 0);
            }
            else
            {
                _LBridgeTran.eulerAngles = new Vector3(_LBridgeTran.eulerAngles.x, _LBridgeTran.eulerAngles.y, 90);
                _RBridgeTran.eulerAngles = new Vector3(_RBridgeTran.eulerAngles.x, _RBridgeTran.eulerAngles.y, -90);
            }
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

                    bool setCollider = false;
                    if (!NowState) setCollider = true;
                    _LBridgeStopCollider.enabled = setCollider;
                    _RBridgeStopCollider.enabled = setCollider;
                    _LBridgeCollider.enabled = !setCollider;
                    _RBridgeCollider.enabled = !setCollider;
                }
            }
        }

        public override void ReceiveInteract()
        {
            //現在橋の状態を変更中なら受け取らない
            if (nowChange) return;

            base.ReceiveInteract();
            nowChange = true;
            ChangeStartTime = Time.time;

            _LBridgeStopCollider.enabled = true;
            _RBridgeStopCollider.enabled = true;
            _LBridgeCollider.enabled = false;
            _RBridgeCollider.enabled = false;
        }
    }
}
