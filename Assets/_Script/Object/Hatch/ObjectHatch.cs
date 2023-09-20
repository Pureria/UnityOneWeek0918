using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectHatchj : ObjectBase
    {
        [SerializeField, Tooltip("�Q�[���J�n���Ƀh�A���J���Ă��邩�HTRUE:�J���Ă��� FALSE:�J���Ă��Ȃ�")]
        private bool InitHatch = false;
        [SerializeField]
        private float HatchChangeTime;

        [SerializeField]
        private GameObject Hatch;

        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitHatch;
            if (NowState) Hatch.SetActive(false);
            else Hatch.SetActive(true);
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();

            ChangeStartTime = Time.time;

            if (NowState) Hatch.SetActive(false);
            else Hatch.SetActive(true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }

}
