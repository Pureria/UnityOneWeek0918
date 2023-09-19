using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MorseGame.UI
{
    public class PopUPWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform myTran;
        [SerializeField] private SpriteRenderer myRenderer;
        [SerializeField] private TextMeshProUGUI _TextMeshPro;
        [SerializeField] private float _Margin = 0.2f;

        private void Update()
        {
            Vector2 size = new Vector2(myRenderer.size.x - _Margin, myRenderer.size.y - _Margin);
            myTran.sizeDelta = size;
        }

        public void SetSize(Vector2 size)
        {
            myRenderer.size = size;
        }

        public void SetText(string text)
        {
           _TextMeshPro.text = text;
        }
    }
}