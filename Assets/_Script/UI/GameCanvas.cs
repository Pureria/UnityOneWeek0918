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
        [SerializeField] private StartCanvas _StartCanvas;
        [SerializeField] private PauseUI _PauseUI;

        public PlayerCanvas PlayerCanvas { get { return _pCanvas; } }
        public MenuPopUP GameClearPopup { get { return _GameClearPopup; } }
        public MenuPopUP GameOverPopup { get { return _GameOverPopup; } }

        public StartCanvas StartCanvas { get { return _StartCanvas; } }
        public PauseUI PauseUI { get { return _PauseUI; } }

        public void GameStart()
        {
            _StartPopup.Hide();
        }

        public void SwitchPauseUI()
        {
            //•\Ž¦’†‚¾‚Á‚½‚ç
            if(_PauseUI.gameObject.activeSelf)
            {
                _PauseUI.HidePauseUI();
            }
            //”ñ•\Ž¦‚¾‚Á‚½‚ç
            else
            {
                _PauseUI.gameObject.SetActive(true);
                _PauseUI.ShowPauseUI();
            }
        }
    }
}