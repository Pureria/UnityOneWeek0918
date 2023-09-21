using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectSwitchingStairs : ObjectBase
    {
        [SerializeField, Tooltip("�Q�[���J�n���ɊK�i�����݂��Ă��邩�HTRUE:���݂��Ă��� FALSE:���݂��Ă��Ȃ�")]
        private bool InitSwitchingStairs = true;
        [SerializeField]
        private float SwitchingStairsChangeTime;

        [SerializeField]
        private GameObject SwitchingStairs;

        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitSwitchingStairs;
            if (NowState) SwitchingStairs.SetActive(true);
            else SwitchingStairs.SetActive(false);
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();

            ChangeStartTime = Time.time;

            if (NowState) SwitchingStairs.SetActive(true);
            else SwitchingStairs.SetActive(false);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }

}


