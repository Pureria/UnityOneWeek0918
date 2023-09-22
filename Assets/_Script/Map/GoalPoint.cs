using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Map
{
    public class GoalPoint : MonoBehaviour
    {
        public Action OnGoalInPlayer;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                OnGoalInPlayer?.Invoke();
            }
        }
    }
}