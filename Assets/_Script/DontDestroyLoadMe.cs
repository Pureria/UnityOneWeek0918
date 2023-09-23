using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Sound
{
    public class DontDestroyLoadMe : MonoBehaviour
    {
        public static DontDestroyLoadMe Instance;
        private void Awake()
        {
            if(Instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
