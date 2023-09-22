using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MorseGame.UI
{
    public class MenuPopUP : MonoBehaviour
    {
        [SerializeField] private RectTransform _PopUpObject;
        [SerializeField] private Vector3 _Scale;
        [SerializeField] private float _ChangeTime = 0.25f;
        [SerializeField] private bool _isStartHide = false;

        private void Start()
        {
            if (_isStartHide) Hide();   
        }

        public void Show()
        {
            _PopUpObject.DOScale(_Scale, _ChangeTime).SetEase(Ease.OutCubic).Play();
        }

        public void Hide()
        {
            _PopUpObject.DOScale(Vector3.zero, _ChangeTime).SetEase(Ease.OutCubic).Play();
        }
    }
}

