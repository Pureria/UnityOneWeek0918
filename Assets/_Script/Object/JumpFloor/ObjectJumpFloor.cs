using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectJumpFloor : ObjectBase
    {
        [SerializeField, Tooltip("ゲーム開始時から踏むと上に飛ばされるか？TRUE:飛ばされる FALSE:飛ばされない")]
        private bool InitJumpFloor = false;

        [SerializeField]
        private GameObject JumpFloor;

        protected override void Start()
        {
            base.Start();

            NowState = InitJumpFloor;
            if (NowState) JumpFloor.SetActive(false);
            else JumpFloor.SetActive(true);
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();

            if (NowState) JumpFloor.SetActive(false);
            else JumpFloor.SetActive(true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }

}
