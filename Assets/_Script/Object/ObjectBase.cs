using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
    protected bool NowState = false;
    public virtual void ReceiveInteract() { NowState = !NowState; }

    public virtual void LogicUpdate() { }

    protected float Animation(float startTime, float endTime, float startKey, float endKey, float nowTime)
    {
        float sKey = startKey;
        float ekey = endKey;
        bool check = false;

        if(startKey > endKey)
        {
            sKey = endKey;
            ekey = startKey;
            check = true;
        }

        float ret = 0.0f;
        float t = endTime - startTime;
        float k = ekey - sKey;
        t = (nowTime - startTime) / t;
        ret = (k * t) + sKey;
        ret = Mathf.Clamp(ret, sKey, ekey);

        if(check)
        {
            ret = (startKey - endKey) - ret;
        }

        return ret;
    }
}
