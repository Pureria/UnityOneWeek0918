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
            //0���E 1���[
            if (morse == 0) text = "�E";
            else if (morse == 1) text = "�[";
            _CurrentText += text;
            ChangeText();
        }

        public void ClearText()
        {
            _CurrentText = "";
            ChangeText();
        }

        /// <summary>
        /// �e�L�X�g�̕�������납������Ă���
        /// </summary>
        /// <param name="delBackCount">������</param>
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
