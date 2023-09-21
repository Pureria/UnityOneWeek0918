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
            if (CheckPlayer.IsTouchPlayer && CoolTime < Time.time)
            {
                Rigidbody2D pbody = CheckPlayer.player.GetComponent<Rigidbody2D>();
                Vector3 pvelo = pbody.velocity;

                pvelo.y = JumpForce;
                pbody.velocity = pvelo;

                CoolTime = Time.time + CoolTimeSec;
            }



        }

    }

}
