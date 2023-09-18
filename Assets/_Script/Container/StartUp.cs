using MorseGame.Map;
using MorseGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.StartUp
{
    public class StartUp : MonoBehaviour
    {
        [SerializeField] private GameObject _DebugMapPrefab;
        [SerializeField] private GameObject _DebugPlayerPrefab;
        [SerializeField] private GameObject _DebugPlayerNPCPrefab;

        private void Awake()
        {
            GameObject InstantMap = Instantiate( _DebugMapPrefab );
            GameObject InstantPlayer = Instantiate( _DebugPlayerPrefab );
            GameObject InStantPlayerNPC = Instantiate(_DebugPlayerNPCPrefab);
            MapInfo mapInfo = null;
            PlayerController pc = null;

            if (!InstantMap.TryGetComponent<MapInfo>(out mapInfo)) Debug.LogError("マップのプレハブにMapInfoがありません");
            if (!InstantPlayer.TryGetComponent<PlayerController>(out pc)) Debug.LogError("プレイヤーのプレハブにPlayerControllerがありません");

            pc.OnSendMorseInput += mapInfo.ObjectManager.ReceiveMorseInput;
            InStantPlayerNPC.transform.position = mapInfo.PlayerSpawnPosition.position;
        }
    }
}

