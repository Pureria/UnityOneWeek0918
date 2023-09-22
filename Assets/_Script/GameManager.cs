using MorseGame.Object.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Manager
{
    public class GameManager : MonoBehaviour
    {
        private bool _nowGame;
        private bool _EndGame;

        public Action OnGameStartAction;
        public Action OnGameEndAction;

        [Header("開始時に入力するモールス (0 = ・)　　(1 = ー)")]
        [SerializeField] private List<MorseData> _StartMorseData = new List<MorseData>();

        private void Start()
        {
            _nowGame = false;
            _EndGame = false;
        }

        public void GameStart()
        {
            _nowGame = true;
            _EndGame = false;
            OnGameStartAction?.Invoke();
        }

        public void GameEnd()
        {
            _nowGame = false;
            _EndGame = true;
            OnGameEndAction?.Invoke();
        }

        public void ReceiveMorseInput(List<MorseData> inputMorseData)
        {
            if (_nowGame || _EndGame) return;
            if (inputMorseData.Count != _StartMorseData.Count) return;

            bool isSame = true;
            for (int i = 0; i < _StartMorseData.Count; i++)
            {
                if (inputMorseData[i].MorseNumber != _StartMorseData[i].MorseNumber)
                {
                    isSame = false;
                    break;
                }
            }

            if (isSame)
            {
                GameStart();
            }
        }
    }
}

