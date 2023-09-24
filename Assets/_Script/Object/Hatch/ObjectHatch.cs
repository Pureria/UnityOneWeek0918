using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MorseGame.Object
{
    public class ObjectHatch : ObjectBase
    {
        [SerializeField, Tooltip("ゲーム開始時にドアが開いているか？TRUE:開いている FALSE:開いていない")]
        private bool InitHatch = false;
        [SerializeField]
        private float HatchChangeTime;

        [SerializeField]
        private BoxCollider2D HatchCollider;

        [SerializeField] private SpriteRenderer _HatchSprite;
        [SerializeField] private float SpriteChangeTime = 0.5f;

        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitHatch;
            if (NowState) HatchCollider.enabled = false;
            else HatchCollider.enabled = true;

            float alpha = 1.0f;
            if (NowState) alpha = 0.2f;
            _HatchSprite.DOFade(alpha, SpriteChangeTime).Play();
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();

            ChangeStartTime = Time.time;

            HatchCollider.enabled = !NowState;

            float alpha = 1.0f;
            if (NowState) alpha = 0.2f;
            _HatchSprite.DOFade(alpha, SpriteChangeTime).Play();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }

}
