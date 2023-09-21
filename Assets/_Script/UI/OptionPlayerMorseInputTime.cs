using MorseGame.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MorseGame.Option
{
    public class OptionPlayerMorseInputTime : MonoBehaviour
    {
        [Header("各種スライドバー")]
        [SerializeField] private float _DebugSetTime = 0.5f;
        [SerializeField] private Slider _MorseLengthTimeBar;
        [SerializeField] private Slider _MorseOneElaseTimeBar;
        [SerializeField] private Slider _MorseAllElaseTimeBar;

        [Header("現在のスライドの値が入るテキスト")]
        [SerializeField] private TextMeshProUGUI _LengthTimeText;
        [SerializeField] private TextMeshProUGUI _OneElaseText;
        [SerializeField] private TextMeshProUGUI _AllElaseText;

        [Header("現在セットされている値が入るテキスト")]
        [SerializeField] private TextMeshProUGUI _SetLengthTimeText;
        [SerializeField] private TextMeshProUGUI _SetOneElaseText;
        [SerializeField] private TextMeshProUGUI _SetAllElaseText;

        [Header("Input確認関連")]
        [SerializeField] private PlayerInputController _InputController;
        [SerializeField] private TextMeshProUGUI _CheckMorseText;

        private float _MorseLengthTime;
        private float _MorseOneElaseTime;
        private float _MorseAllElaseTime;

        private float _MorseLengthTimeBarValue;
        private float _MorseOneElaseTimeBarValue;
        private float _MorseAllElaseTimeBarValue;

        private string MorseLengthTimeKey = PlayerController.MorseLengthTimeKey;
        private string MorseOneDelTimeKey = PlayerController.MorseOneDelTimeKey;
        private string MorseAllDelTimeKey = PlayerController.MorseAllDelTimeKey;

        private bool _IsNowInput;

        private void Start()
        {
            _IsNowInput = false;
            ResetBar();   
        }

        private void Update()
        {
            _MorseLengthTimeBarValue    = _MorseLengthTimeBar.value;
            _MorseOneElaseTimeBarValue  = _MorseOneElaseTimeBar.value;
            _MorseAllElaseTimeBarValue  = _MorseAllElaseTimeBar.value;

            _LengthTimeText.text = _MorseLengthTimeBarValue.ToString();
            _OneElaseText.text = _MorseOneElaseTimeBarValue.ToString();
            _AllElaseText.text = _MorseAllElaseTimeBarValue.ToString();

            float allElaseTime = _MorseLengthTimeBarValue + _MorseOneElaseTimeBarValue + _MorseAllElaseTimeBarValue;
            float oneElaseTime = _MorseLengthTimeBarValue + _MorseOneElaseTimeBarValue;

            if (_IsNowInput)
            {
                float checkTime = Time.time - _InputController.MorseInputStartTime;

                //全消し入力
                if(checkTime > allElaseTime)
                {
                    _CheckMorseText.text = "全消し";
                }
                //一文字消し入力
                else if(checkTime > oneElaseTime)
                {
                    _CheckMorseText.text = "一文字消し";
                }
                //ー入力
                else if(checkTime > _MorseLengthTimeBarValue)
                {
                    _CheckMorseText.text = "ー";
                }
                //・入力
                else
                {
                    _CheckMorseText.text = "・";
                }

                //入力終了時の処理
                if(!_InputController.MorseInput)
                {
                    _IsNowInput = false;
                    _CheckMorseText.text = "Push Space";
                }
            }
            else if (_InputController.MorseInput) _IsNowInput = true;
        }

        public void SetTimeValue()
        {
            PlayerPrefs.SetFloat(MorseLengthTimeKey, _MorseLengthTimeBar.value);
            PlayerPrefs.SetFloat(MorseOneDelTimeKey, _MorseOneElaseTimeBar.value);
            PlayerPrefs.SetFloat(MorseAllDelTimeKey, _MorseAllElaseTimeBar.value);

            _SetLengthTimeText.text = _MorseLengthTimeBar.value.ToString();
            _SetOneElaseText.text = _MorseOneElaseTimeBar.value.ToString();
            _SetAllElaseText.text = _MorseAllElaseTimeBar.value.ToString();
        }

        public void ResetBar()
        {
            if (PlayerPrefs.HasKey(MorseLengthTimeKey)) _MorseLengthTime = PlayerPrefs.GetFloat(MorseLengthTimeKey);
            else
            {
                _MorseLengthTime = _DebugSetTime;
                PlayerPrefs.SetFloat(MorseLengthTimeKey, _DebugSetTime);
            }
            if (PlayerPrefs.HasKey(MorseOneDelTimeKey)) _MorseOneElaseTime = PlayerPrefs.GetFloat(MorseOneDelTimeKey);
            else
            {
                _MorseOneElaseTime = _DebugSetTime;
                PlayerPrefs.SetFloat(MorseOneDelTimeKey, _DebugSetTime);
            }
            if (PlayerPrefs.HasKey(MorseAllDelTimeKey)) _MorseAllElaseTime = PlayerPrefs.GetFloat(MorseAllDelTimeKey);
            else
            {
                _MorseAllElaseTime = _DebugSetTime;
                PlayerPrefs.SetFloat(MorseAllDelTimeKey, _DebugSetTime);
            }

            _MorseLengthTimeBar.value = _MorseLengthTime;
            _MorseOneElaseTimeBar.value = _MorseOneElaseTime;
            _MorseAllElaseTimeBar.value = _MorseAllElaseTime;

            _SetLengthTimeText.text = _MorseLengthTime.ToString();
            _SetOneElaseText.text = _MorseOneElaseTime.ToString();
            _SetAllElaseText.text = _MorseAllElaseTime.ToString();
        }
    }
}
