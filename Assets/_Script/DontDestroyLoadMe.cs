using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Sound
{
    public class DontDestroyLoadMe : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
