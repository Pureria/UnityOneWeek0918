using MorseGame.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace MorseGame.StartUp
{ 
    public class StageSelecter : MonoBehaviour
    {
        [SerializeField] private StageData _DebugStageData;
        [SerializeField] private bool _IsSetDebugStageData = false;
        [SerializeField] private string _StageHashKey = "SelectStageHashKey - ";

        private void Update()
        {
            if(_IsSetDebugStageData)
            {
                _IsSetDebugStageData = false;
                SaveStageFile(_DebugStageData.GetStageBinaryData());
            }
        }

        public void SaveStageFile(StageBinaryData data)
        {
            MemoryStream memoryStream= new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, data);
            byte[] binaryData = memoryStream.ToArray();
            PlayerPrefs.SetString(_StageHashKey, Convert.ToBase64String(binaryData));
            Debug.Log(data.StageName + "Çï€ë∂ÇµÇ‹ÇµÇΩÅB");
        }
    }
}
