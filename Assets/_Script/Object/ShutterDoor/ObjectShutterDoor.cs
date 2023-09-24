using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

namespace MorseGame.Object
{
    public class ObjectShutterDoor : ObjectBase
    {
        [SerializeField, Tooltip("ゲーム開始時にドアが開いているか？TRUE:開いている FALSE:開いていない")]
        private bool InitShutterDoor = false;
        [SerializeField]
        private float ShutterDoorChangeTime;

        [SerializeField] private BoxCollider2D _DoorCollider;
        [SerializeField] private SpriteRenderer _ShutterDoorSprite;
        [SerializeField] private float SpriteChangeTime = 0.5f;

        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitShutterDoor;

            _DoorCollider.enabled = !NowState;
            float alpha = 1.0f;
            if (NowState) alpha = 0.1f;
            _ShutterDoorSprite.DOFade(alpha, SpriteChangeTime).Play();
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();

            ChangeStartTime = Time.time;

            _DoorCollider.enabled = !NowState;
            float alpha = 1.0f;
            if (NowState) alpha = 0.1f;
            _ShutterDoorSprite.DOFade(alpha, SpriteChangeTime).Play();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }


}
