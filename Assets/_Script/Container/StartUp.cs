using MorseGame.Map;
using MorseGame.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace MorseGame.StartUp
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] StageData _DebugStageData;
        [SerializeField] private bool _IsLoadDebugStage;
        [SerializeField] private string _StageHashKey = "SelectStageHashKey - ";

        private void Awake()
        {
            //デバッグステージ呼び出し、またはPlayerPrefsにステージが存在しない場合にデバッグステージ呼び出し
            if(_IsLoadDebugStage || !PlayerPrefs.HasKey(_StageHashKey))
            {
                SetUp(_DebugStageData);
                return;
            }

            string binaryStringData = PlayerPrefs.GetString(_StageHashKey);
            byte[] binaryData = Convert.FromBase64String(binaryStringData);
            MemoryStream memoryStream = new MemoryStream(binaryData);
            BinaryFormatter formatter = new BinaryFormatter();
            StageBinaryData loadedStageData = (StageBinaryData)formatter.Deserialize(memoryStream);
            PlayerPrefs.DeleteKey(_StageHashKey);
            StageData loadData = ScriptableObject.CreateInstance<StageData>();
            loadData = loadData.SetStageData(loadedStageData);
            if (loadedStageData != null) SetUp(loadData);
            else Debug.LogError("ステージデータが見つかりません");
        }

        public void SetUp(StageData data)
        {
            GameObject InstantMap = Instantiate(data.MapPrefab);
            GameObject InstantPlayer = Instantiate(data.PlayerPrefab);
            GameObject InStantPlayerNPC = Instantiate(data.PlayerNPCPrefab);
            MapInfo mapInfo = null;
            PlayerController pc = null;

            if (!InstantMap.TryGetComponent<MapInfo>(out mapInfo)) Debug.LogError("マップのプレハブにMapInfoがありません");
            if (!InstantPlayer.TryGetComponent<PlayerController>(out pc)) Debug.LogError("プレイヤーのプレハブにPlayerControllerがありません");

            pc.OnSendMorseInput += mapInfo.ObjectManager.ReceiveMorseInput;
            pc.OnShowObjectUI += mapInfo.ObjectManager.ReceiveShowMorseUI;
            pc.OnHideObjectUI += mapInfo.ObjectManager.ReceiveHideMorseUI;
            InStantPlayerNPC.transform.position = mapInfo.PlayerSpawnPosition.position;
        }
    }
}

