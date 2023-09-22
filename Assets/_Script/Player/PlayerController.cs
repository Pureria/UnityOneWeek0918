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
        public static string MorseLengthTimeKey = "Key - MorseLengthTime";
        public static string MorseOneDelTimeKey = "Key - MorseOneDelTime";
        public static string MorseAllDelTimeKey = "Key - MorseAllDelTime";

        [SerializeField]                                                private PlayerInputController _InputController;
        [SerializeField, Tooltip("���͏I�����玟�̓��͉��ł̑ҋ@����")] private float _InputWaitTime = 0.5f;
        [SerializeField,Tooltip("�E����[�ɐ؂�ւ��܂ł̎���")]      private float _DebugMorseLengthTime = 0.2f;
        [SerializeField, Tooltip("MorseLenghTime����ꕶ�������ɕς��܂ł̎���")] private float _DebugDelOneMorseTime = 0.5f;
        [SerializeField, Tooltip("�ꕶ����������S�����ɑ���܂ł̎���")] private float _DebugAllDelMorseTime = 0.5f;

        private int nowCount;
        private bool nowInput = false;
        private float _MorseLengthTime = 0.0f;
        private float _MorseOneElaseTime = 0.0f;
        private float _MorseAllElaseTime = 0.0f;
        private List<MorseData> _InputMorseData = new List<MorseData>();

        public Action<List<MorseData>> OnSendMorseInput;
        public Action<ObjectBase> OnShowObjectUI;
        public Action OnHideObjectUI;
        public Action<int> OnAddMorseAction;
        public Action OnClearMorseAction;
        public Action<int> OnDelOneMorseAction;

        private void Start()
        {
            if (PlayerPrefs.HasKey(MorseLengthTimeKey)) _MorseLengthTime = PlayerPrefs.GetFloat(MorseLengthTimeKey);
            else _MorseLengthTime = _DebugMorseLengthTime;
            if (PlayerPrefs.HasKey(MorseOneDelTimeKey)) _MorseOneElaseTime = PlayerPrefs.GetFloat(MorseOneDelTimeKey) + _MorseLengthTime;
            else _MorseOneElaseTime = _DebugDelOneMorseTime + _MorseLengthTime;
            if (PlayerPrefs.HasKey(MorseAllDelTimeKey)) _MorseAllElaseTime = PlayerPrefs.GetFloat(MorseAllDelTimeKey) + _MorseOneElaseTime;
            else _MorseAllElaseTime = _DebugAllDelMorseTime + _MorseOneElaseTime;

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
                    OnClearMorseAction?.Invoke();
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
                float morseCancelLong = _InputController.MorseInputCanceledTime - _InputController.MorseInputStartTime;
                //�S����
                if(morseCancelLong >= _MorseAllElaseTime)
                {
                    nowCount = 0;
                    _InputMorseData.Clear();
                    OnClearMorseAction?.Invoke();
                    nowInput = false;
                    return;
                }
                //�ꕶ������
                else if(morseCancelLong >= _MorseOneElaseTime)
                {
                    nowCount--;
                    _InputMorseData.RemoveAt(_InputMorseData.Count - 1);
                    OnDelOneMorseAction?.Invoke(1);
                }
                //�ǉ�
                else
                {
                    if (morseCancelLong <= _MorseLengthTime) morse = 0;
                    addData.SetMorse = morse;
                    _InputMorseData.Add(addData);
                    OnAddMorseAction?.Invoke(morse);
                    nowCount++;
                }                
                nowInput = false;
            }
        }

        private void CheckMousePosition()
        {
            //TODO::���g�v�ύX
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_InputController.MousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
            bool check = false;

            foreach(RaycastHit2D hit in hits)
            {
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
            }

            if(!check)
            {
                OnHideObjectUI?.Invoke();
            }
        }
    }
}
