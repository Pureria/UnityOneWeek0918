using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBridge : ObjectBase
{
    [SerializeField,Tooltip("�Q�[���J�n���ɋ����˂����Ă��邩�H TRUE�F�˂����Ă���@FALSE : �˂����Ă��Ȃ�")] private bool InitBridge = false;
    [SerializeField] private float BridgeChangeTime;

    [SerializeField] private Transform LBridgeTran;
    [SerializeField] private Transform RBridgeTran;
    [SerializeField] private bool isDebug = false;

    private bool nowChange;
    private float ChangeStartTime;

    private void Start()
    {
        NowState = InitBridge;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if(isDebug)
        {
            isDebug = false;
            ReceiveInteract();
        }

        if(nowChange)
        {
            float startKeyL = 0;
            float endKeyL = 0;
            float startKeyR = 0.0f;
            float endKeyR = 0;
            if (!NowState)
            {
                startKeyL = 0;
                endKeyL = 90.0f;
                startKeyR = 0;
                endKeyR = 90.0f;
            }
            else
            {
                startKeyL = 90.0f;
                endKeyL = 0.0f;
                startKeyR = 90.0f;
                endKeyR = 0.0f;
            }

            float rot = Animation(ChangeStartTime, ChangeStartTime + BridgeChangeTime, startKeyL, endKeyL, Time.time);
            Vector3 euler = LBridgeTran.eulerAngles;
            euler.z = rot;
            LBridgeTran.eulerAngles = euler;

            rot = Animation(ChangeStartTime, ChangeStartTime + BridgeChangeTime, startKeyR, endKeyR, Time.time);
            euler = RBridgeTran.eulerAngles;
            euler.z = rot * -1.0f;
            RBridgeTran.eulerAngles = euler;
            
            if(ChangeStartTime + BridgeChangeTime < Time.time)
            {
                nowChange = false;
                euler = LBridgeTran.eulerAngles;
                euler.z = endKeyL;
                LBridgeTran.eulerAngles = euler;
                euler = RBridgeTran.eulerAngles;
                euler.z = endKeyR * -1.0f;
                RBridgeTran.eulerAngles = euler;
            }
        }
    }

    public override void ReceiveInteract()
    {
        //���݋��̏�Ԃ�ύX���Ȃ�󂯎��Ȃ�
        if (nowChange) return;

        base.ReceiveInteract();
        nowChange = true;
        ChangeStartTime = Time.time;
    }
}
