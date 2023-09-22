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

    EnemyState currentState = EnemyState.Idle;  //Enemy�̌��݂̏�ԁA�����l��Idle�ɂ��Ă���
    private bool stateEnter = true;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool isRight = true;
    [SerializeField] private float _CheckFrontDistance = 1.0f;
    [SerializeField] private float _CheckGroundDistance = 0.2f;
    [SerializeField] private Transform _CheckFrontTran;
    [SerializeField] private Transform _CheckGroundTran;

    private Rigidbody2D myRB;

    private bool isGround;
    private bool isTouchGround;


    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //�O�����̊m�F
        Vector2 origin = _CheckFrontTran.position;
        Vector2 direciton = transform.right;
        if 
        

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
    
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
        stateEnter = false;
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

    private void CheckRotation()
    {
        float dir = 0.0f;
        if (!isRight) dir = 180.0f;
        Vector3 rot = transform.eulerAngles;
        rot.y = dir;
        transform.eulerAngles = rot;
    }

}
