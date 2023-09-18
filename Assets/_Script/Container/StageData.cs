using MorseGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Map
{
    [CreateAssetMenu(fileName = "newMapData", menuName = "Data/Map")]
    public class StageData : ScriptableObject
    {
        public GameObject MapPrefab;
        public GameObject PlayerPrefab;
        public GameObject PlayerNPCPrefab;

        public void SetUp()
        {
            GameObject InstantMap = Instantiate(MapPrefab);
            GameObject InstantPlayer = Instantiate(PlayerPrefab);
            GameObject InStantPlayerNPC = Instantiate(PlayerNPCPrefab);
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
