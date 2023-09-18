using MorseGame.Map;
using MorseGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.StartUp
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] StageData _DebugStageData;

        private void Awake()
        {
            _DebugStageData.SetUp();
        }
    }
}

