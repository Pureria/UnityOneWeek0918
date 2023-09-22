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

        public bool IsAnimationFinished { get { return _IsAnimationFinished; } }

        public void Initialize(Animator animator,PlayerAnimation initAnimation)
        {
            _PlayerAC = animator;
            _NowCurrent = initAnimation;
            _IsAnimationFinished = false;
            ChangeAnimationBool(_NowCurrent);
        }

        public void ChangeAnimationBool(PlayerAnimation changeAnimation)
        {
            _PlayerAC.SetBool(_NowCurrent.ToString(), false);
            _NowCurrent = changeAnimation;
            _PlayerAC.SetBool(_NowCurrent.ToString(), true);
        }

        public void AnimationFinished()
        {
            _IsAnimationFinished = true;
        }

        public void UseAnimationFinishedTrigger() => _IsAnimationFinished = false;
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