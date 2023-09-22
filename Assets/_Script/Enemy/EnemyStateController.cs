using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Enemy
{
    public class EnemyStateController : MonoBehaviour
    {
        public enum EnemyState
        {
            Idle,
            Walk,
            Jump,
            Fall,
            OnGround
        }

        EnemyState currentState = EnemyState.Idle;  //Enemy�̌��݂̏�ԁA�����l��Idle�ɂ��Ă���
        private bool stateEnter = true;     //State���؂�ւ�����Ƃ��Ɉ�x�����������s���Ƃ��Ɏg��

        [SerializeField] private float speed = 1.0f;
        [SerializeField] private bool isRight = true;
        [SerializeField] private float _CheckFrontDistance = 1.0f;
        [SerializeField] private float _CheckGroundRadius = 0.2f;
        [SerializeField] private Transform _CheckFrontTran;
        [SerializeField] private Transform _CheckGroundTran;

        private Rigidbody2D myRB;

        private bool isGround;
        private bool isTouchGround;
        private bool isFinishedAnimation;

        public Action<EnemyState> OnChangeAnimation;


        private void Start()
        {
            if (transform.parent.TryGetComponent<EnemyManager>(out EnemyManager manager))
            {
                manager.AddEnemy(this);
            }
            else
            {
                Debug.LogError("EnemyManager��" + transform.parent.name + "�Ɍ�����܂���B");
            }

            myRB = GetComponent<Rigidbody2D>();
            CheckRotation();
            isGround = false;
            isTouchGround = false;
            isFinishedAnimation = false;
        }

        private void Update()
        {
            Debug.Log(currentState);

            //�O�����̊m�F
            Vector2 origin = _CheckFrontTran.position;
            Vector2 direction = transform.right;
            if (CheckDistance(origin, direction, _CheckFrontDistance))
            {
                isRight = !isRight;
                CheckRotation();
            }

            //�n�ʕ����̊m�F
            if (CheckGround())
            {
                isGround = true;
                isTouchGround = true;
            }
            else
            {
                isGround = false;
                isTouchGround= false;
            }


                switch (currentState)
            {
                case EnemyState.Walk:
                    if (myRB.velocity.y > 0 && !isGround)
                    {
                        ChangeState(EnemyState.Jump);
                        AnimationChanged(EnemyState.Jump);
                        return;
                    }
                    else if (myRB.velocity.y < 0 && !isGround)
                    {
                        ChangeState(EnemyState.Fall);
                        AnimationChanged(EnemyState.Fall);
                        return;
                    }
                    else return;

                case EnemyState.Jump:
                    {
                        if (myRB.velocity.y < 0 && !isGround)
                        {
                            ChangeState(EnemyState.Fall);
                            AnimationChanged(EnemyState.Fall);
                            return;
                        }
                    }
                    break;

                case EnemyState.Fall:
                    { 
                        if (isTouchGround)
                        {
                            ChangeState(EnemyState.OnGround);
                            AnimationChanged(EnemyState.OnGround);
                            return;
                        }
                    }
                    break;

                case EnemyState.OnGround:
                    if(isFinishedAnimation)
                    {
                        isFinishedAnimation = false;
                        ChangeState(EnemyState.Walk);
                        AnimationChanged(EnemyState.Walk);
                    }
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (currentState == EnemyState.Walk)
            {
                Vector2 move = new Vector2(speed * (isRight ? 1 : -1), myRB.velocity.y);
                myRB.velocity = move;
            }
        }
    
        private void ChangeState(EnemyState newState)
        {
            currentState = newState;
            stateEnter = false;
        }

        public void StartGame()
        {
            ChangeState(EnemyState.Walk);
            AnimationChanged(EnemyState.Walk);

            myRB = GetComponent<Rigidbody2D>();
            CheckRotation();
        }

        public void EndGame()
        {
            ChangeState(EnemyState.Idle);
        }

        public void FinishedAnimation()
        {
            isFinishedAnimation = true;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Vector3 to = _CheckFrontTran.position;
            to.x += _CheckFrontDistance * (isRight ? 1 : -1);
            Gizmos.DrawLine(_CheckFrontTran.position, to);
            Gizmos.DrawSphere(_CheckGroundTran.position, _CheckGroundRadius);
        }

        private bool CheckDistance(Vector2 origin, Vector2 direction, float distance)
        {
            //����isTrigger�����Ă��Ȃ��I�u�W�F�N�g�ƐڐG�����珈�����s�ɂȂ��Ă��邪
            //���C���[�ŋ�ʂ����ق�����Ηǂ�����(Prefab�Ƃ����������Ă邩��ύX����̂߂�ǂ�����)
            //�C����������ύX
            bool ret = false;
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance);

            foreach (RaycastHit2D hit in hits)
            {
                Collider2D collider = hit.collider;
                if (!collider.isTrigger && hit.transform != this.transform)
                {
                    ret = true;
                }
            }

            return ret;
        }

        private bool CheckGround()
        {
            bool ret = false;
            Collider2D[] hits = Physics2D.OverlapCircleAll(_CheckGroundTran.position, _CheckGroundRadius);
            foreach (Collider2D hit in hits)
            {
                if (!hit.isTrigger && hit.transform != this.transform)
                {
                    ret = true;
                }
            }
            return ret;
        }

        private void CheckRotation()
        {
            float dir = 0.0f;
            if (!isRight) dir = 180.0f;
            Vector3 rot = transform.eulerAngles;
            rot.y = dir;
            transform.eulerAngles = rot;
        }

        public void AnimationChanged(EnemyState afterTransition)
        {
            OnChangeAnimation?.Invoke(afterTransition);
        }

    }
}
