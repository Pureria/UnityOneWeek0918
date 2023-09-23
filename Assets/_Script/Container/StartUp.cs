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
            //�f�o�b�O�X�e�[�W�Ăяo���A�܂���PlayerPrefs�ɃX�e�[�W�����݂��Ȃ��ꍇ�Ƀf�o�b�O�X�e�[�W�Ăяo��
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
            else Debug.LogError("�X�e�[�W�f�[�^���ϊ��ł��܂���ł����B");
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

            if (!InstantMap.TryGetComponent<MapInfo>(out mapInfo))          Debug.LogError("�}�b�v�̃v���n�u��MapInfo������܂���B");
            if (!InstantPlayer.TryGetComponent<PlayerController>(out pc))   Debug.LogError("�v���C���[�̃v���n�u��PlayerController������܂���B");
            if (!InstantPlayerNPC.TryGetComponent<PlayerNPC>(out pNPC))   Debug.LogError("�v���C���[NPC�̃v���n�u��PlayerNPC������܂���B");
            if (!InstantGameUI.TryGetComponent<GameCanvas>(out gameUI))    Debug.LogError("�Q�[��UI�v���n�u��GameCanvas������܂���B");

            pc.OnSendMorseInput     += mapInfo.ObjectManager.ReceiveMorseInput;
            pc.OnSendMorseInput     += mapInfo.GameManager.ReceiveMorseInput;
            pc.OnShowObjectUI       += mapInfo.ObjectManager.ReceiveShowMorseUI;
            pc.OnInitObjectUI       += mapInfo.ObjectManager.ClearShowMorseUIList;
            pc.OnAddMorseAction     += gameUI.PlayerCanvas.AddText;
            pc.OnClearMorseAction   += gameUI.PlayerCanvas.ClearText;
            pc.OnDelOneMorseAction  += gameUI.PlayerCanvas.DeleteBackText;
            pc.OnNowMorseInputText  += gameUI.PlayerCanvas.NowInput;
            pc.OnSwitchPauseUI      += gameUI.SwitchPauseUI;

            pNPC.OnDeadAction       += mapInfo.GameManager.GameOver;
            pNPC.SetIsPlayerRight(mapInfo.IsPlayerRight);

            mapInfo.GameManager.OnGameStartAction   += pNPC.StartGame;
            mapInfo.GameManager.OnGameStartAction   += gameUI.GameStart;
            mapInfo.GameManager.OnGameStartAction   += mapInfo.EnemyManager.GameStart;
            mapInfo.GameManager.OnGameEndAction     += pNPC.EndGame;
            mapInfo.GameManager.OnGameEndAction     += mapInfo.EnemyManager.GameEnd;
            mapInfo.GameManager.OnGameClearAction   += gameUI.GameClearPopup.Show;
            mapInfo.GameManager.OnGameOverAction    += gameUI.GameOverPopup.Show;
            mapInfo.GoalPoint.OnGoalInPlayer        += mapInfo.GameManager.GameClear;
            mapInfo.FallGround.OnGameOver           += mapInfo.GameManager.GameOver;

            gameUI.PauseUI.SetStartCanvas(gameUI.StartCanvas);

            InstantPlayerNPC.transform.position = mapInfo.PlayerSpawnPosition.position;
        }
    }
}

