using MorseGame.Player;
using MorseGame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private PlayerCanvas _pCanvas;
        [SerializeField] private MenuPopUP _StartPopup;
        [SerializeField] private MenuPopUP _GameClearPopup;
        [SerializeField] private MenuPopUP _GameOverPopup;

        public PlayerCanvas PlayerCanvas { get { return _pCanvas; } }
        public MenuPopUP GameClearPopup { get { return _GameClearPopup; } }

        public void GameStart()
        {
            _StartPopup.Hide();
        }
    }
}