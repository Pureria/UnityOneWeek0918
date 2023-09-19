using MorseGame.Player;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace MorseGame.Map
{
    [CreateAssetMenu(fileName = "newMapData", menuName = "Data/Map")]
    public class StageData : ScriptableObject
    {
        public GameObject MapPrefab;
        public GameObject PlayerPrefab;
        public GameObject PlayerNPCPrefab;
        public GameObject PlayerUIPrefab;
        
        public StageBinaryData GetStageBinaryData()
        {
            string pattern = @"Resources/(.*)\.prefab";

            string mapPath = ExtractPath(AssetDatabase.GetAssetPath(MapPrefab), pattern);
            string PlayerPath = ExtractPath(AssetDatabase.GetAssetPath(PlayerPrefab), pattern);
            string PlayerNPCPath = ExtractPath(AssetDatabase.GetAssetPath(PlayerNPCPrefab), pattern);
            string PlayerUIPath = ExtractPath(AssetDatabase.GetAssetPath(PlayerUIPrefab), pattern);
            return new StageBinaryData(mapPath, PlayerPath, PlayerNPCPath, PlayerUIPath, this.name);
        }

        public StageData SetStageData(StageBinaryData data)
        {
            MapPrefab = Resources.Load<GameObject>(data.MapPrefabPath);
            PlayerPrefab = Resources.Load<GameObject>(data.PlayerPrefabPath);
            PlayerNPCPrefab = Resources.Load<GameObject>(data.PlayerNPCPrefabPath);
            PlayerUIPrefab = Resources.Load<GameObject>(data.PlayerUIPrefabPath);
            return this;
        }

        private string ExtractPath(string input, string pattern)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return null;
            }
        }
    }

    [System.Serializable]
    public class StageBinaryData
    {
        public StageBinaryData(string mapPath,string PlayerPath,string PlayerNPCPath ,string PlayerUIPath, string name)
        {
            MapPrefabPath = mapPath;
            PlayerPrefabPath = PlayerPath;
            PlayerNPCPrefabPath = PlayerNPCPath;
            PlayerUIPrefabPath = PlayerUIPath;
            StageName = name;
        }

        public string MapPrefabPath = "";
        public string PlayerPrefabPath = "";
        public string PlayerNPCPrefabPath = "";
        public string PlayerUIPrefabPath = "";
        public string StageName = "";
    }
}