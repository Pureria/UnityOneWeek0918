using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MorseGame.Player
{
    public class PlayerCanvas : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _MorseInputText;
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

        private void ChangeText()
        {
            _MorseInputText.text = _CurrentText;
        }
    }
}