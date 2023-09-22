using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private EnemyStateController _EStateController;
        [SerializeField] private Animator _Anim;
        private EnemyStateController.EnemyState _CurrentState = EnemyStateController.EnemyState.idle;

        private void Awake()
        {
            _EStateController.OnChangeAnimation += ChangeAnimation;
        }

        private void ChangeAnimation(EnemyStateController.EnemyState state)
        {
            _Anim.SetBool(_CurrentState.ToString(), false);
            _CurrentState = state;
            _Anim.SetBool(_CurrentState.ToString(), true);
        }

        public void AnimationFinishedTrigger() => _EStateController.FinishedAnimation();
    }
}
