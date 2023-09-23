using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MorseGame.Player
{
    public class PlayerNPC : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private float airSpeed = 0.5f;
        [SerializeField] private bool isRight = true;
        [SerializeField] private float _CheckFrontDistance = 1.0f;
        [SerializeField] private float _CheckGroundRadius = 0.2f;
        [SerializeField] private Transform _CheckFrontTran;
        [SerializeField] private Transform _CheckGroundTran;

        [Header("アニメーション関連")]
        [SerializeField] private PlayerNPCAnimationController _AnimationController;
        [SerializeField] private Animator _PlayerAC;
        [SerializeField] private PlayerAnimation _InitAnimation = PlayerAnimation.idle;

        [Header("デバッグ用（使わない場合はすべてfalse）")]
        [SerializeField] private bool _DebugInGame;

        public Action OnDeadAction;

        private Rigidbody2D myRB;

        private bool isGround;
        private bool isTouchGround;
        private bool isInGame;
        private void Start()
        {
            myRB = GetComponent<Rigidbody2D>();
            CheckRotation();
            isGround = false;
            isTouchGround = false;
            _AnimationController.Initialize(_PlayerAC, _InitAnimation, false);

            if (_DebugInGame) StartGame();
        }

        private void Update()
        {
            //前方向の確認
            Vector2 origin = _CheckFrontTran.position;
            Vector2 direction = transform.right;
            if(CheckDistance(origin,direction,_CheckFrontDistance))
            {
                isRight = !isRight;
                CheckRotation();
            }

            //地面方向の確認
            if (CheckGround())
            {
                //落下状態から着地状態に遷移
                if(!isGround && _AnimationController.NowCurrent == PlayerAnimation.fall)
                {
                    isTouchGround = true;
                    _AnimationController.ChangeAnimationBool(PlayerAnimation.onGround);
                }
                else if(_AnimationController.NowCurrent != PlayerAnimation.onGround)
                {
                    isGround = true;
                }
            }
            else
            {
                //落下 or ジャンプ状態に遷移
                if (myRB.velocity.y > 0) _AnimationController.ChangeAnimationBool(PlayerAnimation.jump);
                else _AnimationController.ChangeAnimationBool(PlayerAnimation.fall);
                isGround = false;
            }

            //着地モーションが終わったらwalkにチェンジ
            if(isTouchGround && _AnimationController.IsAnimationFinished)
            {
                _AnimationController.UseAnimationFinishedTrigger();
                _AnimationController.ChangeAnimationBool(PlayerAnimation.walk);
                isTouchGround = false;
                isGround = true;
            }

            CheckRotation();
        }

        private void FixedUpdate()
        {
            if (!isInGame) return;

            if(isGround)
            {
                Vector2 move = new Vector2(speed * (isRight ? 1 : -1), myRB.velocity.y);
                myRB.velocity = move;
            }
            else
            {
                Vector2 move = new Vector2(airSpeed * (isRight ? 1 : -1), myRB.velocity.y);
                myRB.velocity = move;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 to = _CheckFrontTran.position;
            to.x += _CheckFrontDistance * (isRight ? 1 : -1);
            Gizmos.DrawLine(_CheckFrontTran.position, to);
            Gizmos.DrawSphere(_CheckGroundTran.position, _CheckGroundRadius);
        }

        private bool CheckDistance(Vector2 origin,Vector2 direction,float distance)
        {
            //現在isTriggerがついていないオブジェクトと接触したら処理実行になっているが
            //レイヤーで区別したほうが絶対良さそう(Prefabとか作っちゃってるから変更するのめんどくさい)
            //気が向いたら変更
            bool ret = false;
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance);

            foreach (RaycastHit2D hit in hits)
            {
                Collider2D collider = hit.collider;
                if (!collider.isTrigger && !collider.CompareTag("Player") && !collider.CompareTag("Enemy"))
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

        public void StartGame()
        {
            isInGame = true;
            _AnimationController.SetCanChangeAnim(true);
            _AnimationController.ChangeAnimationBool(PlayerAnimation.walk);
        }

        public void EndGame()
        {
            isInGame = false;
            _AnimationController.ChangeAnimationBool(PlayerAnimation.idle);
            _AnimationController.SetCanChangeAnim(false);
        }

        public void Dead()
        {
            OnDeadAction?.Invoke();
        }
    }
}
