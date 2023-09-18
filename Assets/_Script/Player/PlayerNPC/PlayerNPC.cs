using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Player
{
    public class PlayerNPC : MonoBehaviour
    {
        [SerializeField] private float speed = 1.0f;
        [SerializeField] private bool isRight = false;
        private Rigidbody2D myRB;

        private void Start()
        {
            myRB = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 move = new Vector2(speed * (isRight ? -1 : 1), myRB.velocity.y);
            myRB.velocity = move;
        }
    }
}
