using MorseGame.Map;
using MorseGame.Player;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using MorseGame.UI;

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
            StageData loadData = ScriptableObject.CreateInstance<StageData>();
            if (loadedStageData != null)
            {
                loadData = loadData.SetStageData(loadedStageData);
                SetUp(loadData);
            }
            else Debug.LogError("ステージデータが変換できませんでした。");
        }

        public void SetUp(StageData data)
        {
            GameObject InstantMap = Instantiate(data.MapPrefab);
            GameObject InstantPlayer = Instantiate(data.PlayerPrefab);
            GameObject InstantPlayerNPC = Instantiate(data.PlayerNPCPrefab);
            GameObject InstantGameUI = Instantiate(data.GameUIPrefab);
            MapInfo mapInfo = null;
            PlayerController pc = null;
            PlayerNPC pNPC = null;
            GameCanvas gameUI = null;

            if (!InstantMap.TryGetComponent<MapInfo>(out mapInfo))          Debug.LogError("マップのプレハブにMapInfoがありません。");
            if (!InstantPlayer.TryGetComponent<PlayerController>(out pc))   Debug.LogError("プレイヤーのプレハブにPlayerControllerがありません。");
            if (!InstantPlayerNPC.TryGetComponent<PlayerNPC>(out pNPC))   Debug.LogError("プレイヤーNPCのプレハブにPlayerNPCがありません。");
            if (!InstantGameUI.TryGetComponent<GameCanvas>(out gameUI))    Debug.LogError("ゲームUIプレハブにGameCanvasがありません。");

            pc.OnSendMorseInput     += mapInfo.ObjectManager.ReceiveMorseInput;
            pc.OnSendMorseInput     += mapInfo.GameManager.ReceiveMorseInput;
            pc.OnShowObjectUI       += mapInfo.ObjectManager.ReceiveShowMorseUI;
            pc.OnHideObjectUI       += mapInfo.ObjectManager.ReceiveHideMorseUI;
            pc.OnAddMorseAction     += gameUI.PlayerCanvas.AddText;
            pc.OnClearMorseAction   += gameUI.PlayerCanvas.ClearText;
            pc.OnDelOneMorseAction  += gameUI.PlayerCanvas.DeleteBackText;
            pc.OnNowMorseInputText  += gameUI.PlayerCanvas.NowInput;

            mapInfo.GameManager.OnGameStartAction   += pNPC.StartGame;
            mapInfo.GameManager.OnGameStartAction   += gameUI.GameStart;
            mapInfo.GameManager.OnGameEndAction     += pNPC.EndGame;

            InstantPlayerNPC.transform.position = mapInfo.PlayerSpawnPosition.position;
        }
    }
}

