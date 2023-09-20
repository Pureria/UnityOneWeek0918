using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _FillImage;
    [SerializeField] private TextMeshProUGUI _TMP;
    [SerializeField] private Color _RollOverTextColor;
    private Color _BaseTextColor;

    private void Start()
    {
        _FillImage.fillAmount = 0;
        _BaseTextColor = _TMP.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _FillImage.DOFillAmount(1f, 0.25f).SetEase(Ease.OutCubic).Play();
        _TMP.DOColor(_RollOverTextColor, 0.25f).Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _FillImage.DOFillAmount(0, 0.25f).SetEase(Ease.OutCubic).Play();
        _TMP.DOColor(_BaseTextColor, 0.25f).Play();
    }
}
