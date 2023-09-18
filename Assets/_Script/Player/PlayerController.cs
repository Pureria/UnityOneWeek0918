using MorseGame.Object;
using MorseGame.Object.Data;
using MorseGame.Object.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]                                                private PlayerInputController _InputController;
        [SerializeField, Tooltip("���͏I�����玟�̓��͉��ł̑ҋ@����")] private float _InputWaitTime = 0.5f;
        [SerializeField,Tooltip("�E����[�ɐ؂�ւ��܂ł̎���")]      private float _DebugMorseLengthTime = 0.2f;

        private int nowCount;
        private bool nowInput = false;
        private float _MorseLengthTime = 0.0f;
        private List<MorseData> _InputMorseData = new List<MorseData>();

        public Action<List<MorseData>> OnSendMorseInput;
        public Action<ObjectBase> OnShowObjectUI;
        public Action OnHideObjectUI;

        private void Start()
        {
            //TODO::���Ƃ���ݒ�Ő؂�ւ��悤�ɕύX
            _MorseLengthTime = _DebugMorseLengthTime;
            _InputMorseData.Clear();
            nowCount = 0;
            nowInput = false;
        }

        private void Update()
        {
            CheckInput();
            CheckMousePosition();
        }

        private void CheckInput()
        {
            //���ɓ��͂����݂���莞�Ԍo�߂���Ɠ��͂��C�x���g�ɓ`���Ē��g���폜����
            if (nowCount != 0 && !nowInput)
            {
                if (_InputController.MorseInputCanceledTime + _InputWaitTime < Time.time)
                {
                    OnSendMorseInput?.Invoke(_InputMorseData);
                    string debug = "";
                    for (int i = 0; i < _InputMorseData.Count; i++)
                    {
                        if (_InputMorseData[i].MorseNumber == 0) debug += "�E";
                        else debug += "�[";
                    }
                    Debug.Log(debug);

                    nowCount = 0;
                    _InputMorseData.Clear();
                }
            }

            //���ݓ��͒�����Ȃ��A���͂��������ꍇ
            if (_InputController.MorseInput && !nowInput)
            {
                nowInput = true;
            }
            //���ݓ��͒��ŁA���͂��Ȃ��Ȃ����ꍇ
            else if (!_InputController.MorseInput && nowInput)
            {
                MorseData addData = new MorseData();
                int morse = 1;
                if (_InputController.MorseInputCanceledTime - _InputController.MorseInputStartTime <= _MorseLengthTime) morse = 0;
                addData.SetMorse = morse;
                _InputMorseData.Add(addData);
                nowCount++;
                nowInput = false;
            }
        }

        private void CheckMousePosition()
        {
            //TODO::���g�v�ύX
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_InputController.MousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            bool check = false;

            if (hit.collider != null)
            {
                int objectLayer = LayerMask.NameToLayer("Object");
                if (hit.transform.gameObject.layer == objectLayer)
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    if(clickedObject.TryGetComponent<ObjectBase>(out ObjectBase obj))
                    {
                        OnShowObjectUI?.Invoke(obj);
                        check = true;
                    }
                }
            }

            if(!check)
            {
                OnHideObjectUI?.Invoke();
            }
        }
    }
}