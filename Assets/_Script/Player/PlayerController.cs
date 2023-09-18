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
        [SerializeField, Tooltip("入力終了から次の入力下での待機時間")] private float _InputWaitTime = 0.5f;
        [SerializeField,Tooltip("・からーに切り替わるまでの時間")]      private float _DebugMorseLengthTime = 0.2f;

        private int nowCount;
        private bool nowInput = false;
        private float _MorseLengthTime = 0.0f;
        private List<MorseData> _InputMorseData = new List<MorseData>();

        public Action<List<MorseData>> OnSendMorseInput;

        private void Start()
        {
            //TODO::あとから設定で切り替わるように変更
            _MorseLengthTime = _DebugMorseLengthTime;
            _InputMorseData.Clear();
            nowCount = 0;
            nowInput = false;
        }

        private void Update()
        {
            //既に入力が存在し一定時間経過すると入力をイベントに伝えて中身を削除する
            if(nowCount != 0 && !nowInput)
            {
                if(_InputController.MorseInputCanceledTime + _InputWaitTime < Time.time)
                {
                    OnSendMorseInput?.Invoke(_InputMorseData);
                    string debug = "";
                    for(int i = 0;i<_InputMorseData.Count;i++)
                    {
                        if (_InputMorseData[i].MorseNumber == 0) debug += "・";
                        else debug += "ー";
                    }
                    Debug.Log(debug);

                    nowCount = 0;
                    _InputMorseData.Clear();
                }
            }

            //現在入力中じゃなく、入力があった場合
            if(_InputController.MorseInput && !nowInput)
            {
                nowInput = true;
            }
            //現在入力中で、入力がなくなった場合
            else if(!_InputController.MorseInput && nowInput)
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
    }
}
