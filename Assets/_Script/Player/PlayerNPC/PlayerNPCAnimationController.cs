using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Player
{
    public class PlayerNPCAnimationController : MonoBehaviour
    {
        private Animator _PlayerAC;
        private PlayerAnimation _NowCurrent;
        private bool _IsAnimationFinished;
        private bool _CanChangeAnimation;

        public PlayerAnimation NowCurrent { get { return _NowCurrent; } }
        public bool IsAnimationFinished { get { return _IsAnimationFinished; } }

        public void Initialize(Animator animator,PlayerAnimation initAnimation,bool CanChangeAnimation)
        {
            _PlayerAC = animator;
            _NowCurrent = initAnimation;
            _IsAnimationFinished = false;
            _CanChangeAnimation = CanChangeAnimation;
            ChangeAnimationBool(_NowCurrent);
        }

        public void ChangeAnimationBool(PlayerAnimation changeAnimation)
        {
            if (!_CanChangeAnimation) return;

            _PlayerAC.SetBool(_NowCurrent.ToString(), false);
            _NowCurrent = changeAnimation;
            _PlayerAC.SetBool(_NowCurrent.ToString(), true);
        }

        public void AnimationFinished()
        {
            _IsAnimationFinished = true;
        }

        public void UseAnimationFinishedTrigger() => _IsAnimationFinished = false;

        public void SetCanChangeAnim(bool flg) => _CanChangeAnimation = flg;
    }

    public enum PlayerAnimation
    {
        idle,
        walk,
        jump,
        fall,
        onGround
    }
}