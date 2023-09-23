using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace MorseGame.UI
{
    public class StartCanvas : MonoBehaviour
    {
        [SerializeField] private List<Image> _FillImages = new List<Image>();
        [SerializeField] private float _FillTime = 0.25f;
        [SerializeField] private float _ShiftTime = 0.2f;

        [SerializeField] private string _TitleSceneName = "TitleScene";
        [SerializeField] private string _GameSceneName = "GameScene";

        [SerializeField] private AudioSource _AudioSource;
        private void Start()
        {
            StartCoroutine(Show());
        }

        private IEnumerator Show()
        {
            _AudioSource.Play();
            foreach (Image img in _FillImages)
            {
                img.DOFillAmount(0f, _FillTime).SetEase(Ease.OutCubic).Play();
                yield return new WaitForSeconds(_ShiftTime);
            }
        }

        private IEnumerator Hide()
        {
            for (int i = _FillImages.Count - 1; i >= 0; i--)
            {
                _FillImages[i].DOFillAmount(1f, _FillTime).SetEase(Ease.OutCubic).Play();
                yield return new WaitForSeconds(_ShiftTime);
            }
        }

        private IEnumerator Hide(Action endAction)
        {
            for(int i = _FillImages.Count - 1;i >= 0;i--)
            {
                _FillImages[i].DOFillAmount(1f, _FillTime).SetEase(Ease.OutCubic).Play();
                yield return new WaitForSeconds(_ShiftTime);
            }
            yield return new WaitForSeconds(_ShiftTime + 1.0f);
            endAction();
        }

        public void ShowUI() => StartCoroutine(Show());
        public void HideUI() => StartCoroutine(Hide());
        public void HideUIAndMoveTitle() => StartCoroutine(Hide(() => SceneManager.LoadScene(_TitleSceneName)));
        public void HideUIAndMoveGame() => StartCoroutine(Hide(() => SceneManager.LoadScene(_GameSceneName)));

        public void DebugChangeScene() => SceneManager.LoadScene(1);
    }    
}
