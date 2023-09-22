using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MorseGame.UI
{
    public class StartCanvas : MonoBehaviour
    {
        [SerializeField] private List<Image> _FillImages = new List<Image>();
        [SerializeField] private float _FillTime = 0.25f;
        [SerializeField] private float _ShiftTime = 0.2f;
        private void Start()
        {
            StartCoroutine(Show());
        }

        private IEnumerator Show()
        {
            foreach (Image img in _FillImages)
            {
                img.DOFillAmount(0f, _FillTime).SetEase(Ease.OutCubic).Play();
                yield return new WaitForSeconds(_ShiftTime);
            }
        }

        private void Hide()
        {
            foreach (Image img in _FillImages)
            {
                img.DOFillAmount(1f, _FillTime).SetEase(Ease.OutCubic).Play();
            }
        }
    }    
}
