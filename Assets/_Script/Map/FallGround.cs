using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Map
{
    public class FallGround : MonoBehaviour
    {
        public Action OnGameOver;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                OnGameOver?.Invoke();
            }
        }
    }
}

