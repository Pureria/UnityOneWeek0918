using MorseGame.Player;
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
            idle,
            walk,
            jump,
            fall,
            onGround
        }

        EnemyState currentState = EnemyState.idle;  //Enemyの現在の状態、初期値はIdleにしてある
        //private bool stateEnter = true;     //Stateが切り替わったときに一度だけ処理を行うときに使う

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
                Debug.LogError("EnemyManagerが" + transform.parent.name + "に見つかりません。");
            }

            myRB = GetComponent<Rigidbody2D>();
            CheckRotation();
            isGround = false;
            isTouchGround = false;
            isFinishedAnimation = false;
            AnimationChanged(currentState);
        }

        private void Update()
        {
            Debug.Log(currentState);

            //前方向の確認
            Vector2 origin = _CheckFrontTran.position;
            Vector2 direction = transform.right;
            if (CheckDistance(origin, direction, _CheckFrontDistance))
            {
                isRight = !isRight;
                CheckRotation();
            }

            //地面方向の確認
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
                case EnemyState.walk:
                    if (myRB.velocity.y > 0 && !isGround)
                    {
                        ChangeState(EnemyState.jump);
                        AnimationChanged(EnemyState.jump);
                        return;
                    }
                    else if (!isGround)
                    {
                        ChangeState(EnemyState.fall);
                        AnimationChanged(EnemyState.fall);
                        return;
                    }
                    else return;

                case EnemyState.jump:
                    {
                        if (myRB.velocity.y < 0 && !isGround)
                        {
                            ChangeState(EnemyState.fall);
                            AnimationChanged(EnemyState.fall);
                            return;
                        }
                    }
                    break;

                case EnemyState.fall:
                    { 
                        if (isTouchGround)
                        {
                            ChangeState(EnemyState.onGround);
                            AnimationChanged(EnemyState.onGround);
                            return;
                        }
                    }
                    break;

                case EnemyState.onGround:
                    if(isFinishedAnimation)
                    {
                        isFinishedAnimation = false;
                        ChangeState(EnemyState.walk);
                        AnimationChanged(EnemyState.walk);
                    }
                    break;
            }
        }

        private void FixedUpdate()
        {
            if (currentState == EnemyState.walk)
            {
                Vector2 move = new Vector2(speed * (isRight ? 1 : -1), myRB.velocity.y);
                myRB.velocity = move;
            }
        }
    
        private void ChangeState(EnemyState newState)
        {
            currentState = newState;
            //stateEnter = false;
        }

        public void StartGame()
        {
            ChangeState(EnemyState.walk);
            AnimationChanged(EnemyState.walk);

            myRB = GetComponent<Rigidbody2D>();
            CheckRotation();
        }

        public void EndGame()
        {
            ChangeState(EnemyState.idle);
            AnimationChanged(currentState);
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
            //現在isTriggerがついていないオブジェクトと接触したら処理実行になっているが
            //レイヤーで区別したほうが絶対良さそう(Prefabとか作っちゃってるから変更するのめんどくさい)
            //気が向いたら変更
            bool ret = false;
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance);

            foreach (RaycastHit2D hit in hits)
            {
                Collider2D collider = hit.collider;
                if (!collider.isTrigger && !collider.CompareTag("Player") && collider.transform != this.transform)
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.CompareTag("Player"))
            {
                if(collision.transform.TryGetComponent<PlayerNPC>(out PlayerNPC pNpc))
                {
                    pNpc.Dead();
                }
            }
        }
    }
}
