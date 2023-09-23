using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

namespace MorseGame.Object
{
    public class ObjectSwitchingStairs : ObjectBase
    {
        [SerializeField, Tooltip("ゲーム開始時に階段が存在しているか？TRUE:存在している FALSE:存在していない")]
        private bool InitSwitchingStairs = true;
        [SerializeField]
        private float SwitchingStairsChangeTime;

        [SerializeField]
        private GameObject SwitchingStairs;

        [SerializeField] private SpriteRenderer _StairSprite;
        private float ChangeStartTime;

        protected override void Start()
        {
            base.Start();

            NowState = InitSwitchingStairs;
            if (NowState) SwitchingStairs.SetActive(true);
            else SwitchingStairs.SetActive(false);

            float alpha = 0.5f;
            if (NowState) alpha = 1.0f;
            Color set = _StairSprite.color;
            set.a = alpha;
            _StairSprite.color = set;
        }

        public override void ReceiveInteract()
        {
            base.ReceiveInteract();

            ChangeStartTime = Time.time;

            if (NowState) SwitchingStairs.SetActive(true);
            else SwitchingStairs.SetActive(false);

            float alpha = 0.0f;
            if (NowState) alpha = 1.0f;
            else alpha = 0.5f;
            //DOTween.ToAlpha(() => _StairSprite.color, color => _StairSprite.color = color, alpha, SwitchingStairsChangeTime);
            _StairSprite.DOFade(alpha, SwitchingStairsChangeTime).Play();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }
    }

}


