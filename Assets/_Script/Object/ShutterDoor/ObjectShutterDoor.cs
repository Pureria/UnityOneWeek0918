using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectShutterDoor : ObjectBase
    {
        [SerializeField, Tooltip("�Q�[���J�n���Ƀh�A���J���Ă��邩�HTRUE:�J���Ă��� FALSE:�J���Ă��Ȃ�")]
        private bool InitShutterDoor = false;
        [SerializeField]
        private float ShutterDoorChangeTime;

        [SerializeField]
        private GameObject ShutterDoor;

        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitShutterDoor;
            if (NowState) ShutterDoor.SetActive(false);
            else ShutterDoor.SetActive(true);
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();

            ChangeStartTime = Time.time;

            if (NowState) ShutterDoor.SetActive(false);
            else ShutterDoor.SetActive(true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }


}
