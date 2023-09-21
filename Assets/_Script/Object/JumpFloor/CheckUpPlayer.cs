using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object
{
    public class CheckUpPlayer : ObjectBase
    {
        private bool isTouchPlyer = false;
        private Transform Player;


        public bool IsTouchPlayer { get { return isTouchPlyer; } }
        public Transform player { get { return Player; } }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            isTouchPlyer = true;

            Player = collision.transform;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isTouchPlyer = false;
        }

    }

}
