using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object
{
    public class CheckUpPlayer : MonoBehaviour
    {
        private bool isTouchAny = false;
        //private Transform Player;
        private List<Transform> _TouchTrances = new List<Transform>();


        public bool IsTouchAny { get { return isTouchAny; } }
        public List<Transform> TouchTrances{ get { return _TouchTrances; } }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            isTouchAny = true;

            if(!_TouchTrances.Contains(collision.transform))
            {
                _TouchTrances.Add(collision.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isTouchAny = false;

            if(_TouchTrances.Contains(collision.transform))
            {
                _TouchTrances.Remove(collision.transform);
            }
        }

    }

}
