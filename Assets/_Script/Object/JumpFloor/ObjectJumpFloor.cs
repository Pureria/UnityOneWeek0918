using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectJumpFloor : ObjectBase
    {
        [SerializeField, Tooltip("ゲーム開始時から踏むと上に飛ばされるか？TRUE:飛ばされる FALSE:飛ばされない")]
        private bool InitJumpFloor = false;
        [SerializeField]
        private GameObject JumpFloor;
        [SerializeField]
        private float JumpForce;
        [SerializeField]
        private float CoolTimeSec;
        [SerializeField]
        private CheckUpPlayer CheckPlayer;

        private float CoolTime;

        protected override void Start()
        {
            base.Start();

            CoolTime = 0.0f;
            NowState = InitJumpFloor;
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!NowState) return;
            if (CheckPlayer.IsTouchAny && CoolTime < Time.time)
            {
                foreach(Transform tran in CheckPlayer.TouchTrances)
                {
                    Rigidbody2D body = null;
                    if (!tran.TryGetComponent<Rigidbody2D>(out body)) return;

                    Vector3 velo = body.velocity;
                    velo.y = JumpForce;
                    body.velocity = velo;
                }
                CoolTime = Time.time + CoolTimeSec;
            }
        }

    }

}
