using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MorseGame.Player
{
    public class PlayerCanvas : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _MorseInputText;
        [SerializeField] TextMeshProUGUI _NowInputText;
        private string _CurrentText = "";

        public void AddText(int morse)
        {
            string text = "";
            //0が・ 1がー
            if (morse == 0) text = "・";
            else if (morse == 1) text = "ー";
            _CurrentText += text;
            ChangeText();
        }

        public void ClearText()
        {
            _CurrentText = "";
            ChangeText();
        }

        /// <summary>
        /// テキストの文字を後ろから消していく
        /// </summary>
        /// <param name="delBackCount">消す数</param>
        public void DeleteBackText(int delBackCount)
        {
            if (_CurrentText.Length < delBackCount) delBackCount = _CurrentText.Length;
            _CurrentText = _CurrentText.Remove(_CurrentText.Length - delBackCount);
            ChangeText();
        }
        public void NowInput(string text)
        {
            _NowInputText.text = text;
        }

        private void ChangeText()
        {
            _MorseInputText.text = _CurrentText;
        }

    }
}
