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

        [Header("�J�n���ɓ��͂��郂�[���X (0 = �E)�@�@(1 = �[)")]
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

        public void GameClear()
        {
            GameEnd();
        }

        public void GameOver()
        {
            GameEnd();
        }

        private void GameEnd()
        {
            _nowGame = false;
            _EndGame = true;
            OnGameEndAction?.Invoke();
            Debug.Log("�Q�[���I��");
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

