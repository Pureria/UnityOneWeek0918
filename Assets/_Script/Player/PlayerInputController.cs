using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private bool _MorseInput = false;
    private float _MorseInputStartTime = 0.0f;
    private float _MorseInputCanceledTime = 0.0f;

    public bool MorseInput { get { return _MorseInput; } }
    public float MorseInputStartTime { get { return _MorseInputStartTime; } }
    public float MorseInputCanceledTime { get { return _MorseInputCanceledTime; } }

    public void OnMorseInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _MorseInput = true;
            _MorseInputStartTime = Time.time;
        }

        if(context.canceled)
        {
            _MorseInput = false;
            _MorseInputCanceledTime = Time.time;
        }
    }
}
