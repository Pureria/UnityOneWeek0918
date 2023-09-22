using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Walk,
        Jump,
        Fall,
        OnGround
    }

    EnemyState currentState = EnemyState.Idle;  //Enemyの現在の状態、初期値はIdleにしてある
    private bool stateEnter = true;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool isRight = true;
    [SerializeField] private float _CheckFrontDistance = 1.0f;
    [SerializeField] private Transform _CheckFrontTran;

    private Rigidbody2D myRB;


    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
        stateEnter = false;
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckDistance();
        CheckRotation();

        switch (currentState)
        {
            case EnemyState.Walk:
                if (myRB.velocity.y >= 0 && !)
                {
                    
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        Vector2 move = new Vector2(speed * (isRight ? 1 : -1), myRB.velocity.y);
        myRB.velocity = move;
    }

    public void StartGame()
    {
        ChangeState(EnemyState.Walk);
        myRB = GetComponent<Rigidbody2D>();
        CheckRotation();
    }

    public void EndGame()
    {
        ChangeState(EnemyState.Idle);
    }

    public void FinishedAnimation()
    {

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 to = _CheckFrontTran.position;
        to.x += _CheckFrontDistance * (isRight ? -1 : 1);
        Gizmos.DrawLine(_CheckFrontTran.position, to);
    }

    private void CheckDistance()
    {
        //現在isTriggerがついていないオブジェクトと接触したら処理実行になっているが
        //レイヤーで区別したほうが絶対良さそう(Prefabとか作っちゃってるから変更するのめんどくさい)
        //気が向いたら変更
        Vector2 origin = _CheckFrontTran.position;
        Vector2 direction = transform.right;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, _CheckFrontDistance);

        foreach (RaycastHit2D hit in hits)
        {
            Collider2D collider = hit.collider;
            if (!collider.isTrigger && hit.transform != this.transform)
            {
                isRight = !isRight;
                CheckRotation();
            }
        }
    }

    private void CheckRotation()
    {
        float dir = 0.0f;
        if (!isRight) dir = 180.0f;
        Vector3 rot = transform.eulerAngles;
        rot.y = dir;
        transform.eulerAngles = rot;
    }

}
