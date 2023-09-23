using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutTimeScale : MonoBehaviour
{
    [SerializeField] private bool _IsChangeTimeScale = false;

    private void Update()
    {
        if(_IsChangeTimeScale)
        {
            _IsChangeTimeScale = false;
            if(Time.timeScale > 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
