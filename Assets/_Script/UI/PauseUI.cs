using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] private Animator _Anim;

        private float _NowTimeScale;
        private StartCanvas _StartCanvas;

        private void Start()
        {
            _Anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            _NowTimeScale = Time.timeScale;
            this.gameObject.SetActive(false);
        }

        public void ShowPauseUI()
        {
            _NowTimeScale = Time.timeScale;
            Time.timeScale = 0;
            _Anim.SetTrigger("SwitchShowUI");
        }

        public void HidePauseUI()
        {
            Time.timeScale = _NowTimeScale;
            _Anim.SetTrigger("SwitchHideUI");
        }

        public void ReStart()
        {
            Time.timeScale = _NowTimeScale;
            _StartCanvas.HideUIAndMoveGame();
        }

        public void ReturnTitle()
        {
            Time.timeScale = _NowTimeScale;
            _StartCanvas.HideUIAndMoveTitle();
        }

        public void SetStartCanvas(StartCanvas canvas) => _StartCanvas = canvas;

        public void SetActicaFalse()
        {
            this.gameObject.SetActive(false);
        }
    }
}

