using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MorseGame.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private bool _MorseInput = false;
        private bool _PauseInput = false;
        private float _MorseInputStartTime = 0.0f;
        private float _MorseInputCanceledTime = 0.0f;

        private Vector2 _MousePosition;

        public bool MorseInput { get { return _MorseInput; } }
        public float MorseInputStartTime { get { return _MorseInputStartTime; } }
        public float MorseInputCanceledTime { get { return _MorseInputCanceledTime; } }
        public Vector2 MousePosition { get { return _MousePosition; } }
        public bool PauseInput { get { return _PauseInput; } }

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

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            _MousePosition = context.ReadValue<Vector2>();
        }

        public void OnPauseInput(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                _PauseInput = true;
            }
        }

        public void UsePauseInput() => _PauseInput = false;
    }
}
