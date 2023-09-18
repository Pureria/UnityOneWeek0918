using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Object.Data
{
    [CreateAssetMenu(fileName = "newMorseObjectData", menuName = "Data/Object")]
    public class ObjectMorseData : ScriptableObject
    {
        [Header("(0 = ・)　　(1 = ー)")]
        public List<MorseData> MorseData = new List<MorseData>();
    }

    [System.Serializable]
    public class MorseData
    {
        //0 = ・
        //1 = ー        
        [SerializeField, Range(0, 1)] private int _Morse;
        [HideInInspector] public int SetMorse { set { _Morse = value; } }
        public int MorseNumber { get { return _Morse; }}
    }
}
