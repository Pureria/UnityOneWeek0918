using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectShutterDoor : ObjectBase
    {
        [SerializeField, Tooltip("ゲーム開始時にドアが開いているか？TRUE:開いている FALSE:開いていない")]
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
