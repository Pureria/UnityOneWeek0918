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
        
        public StageBinaryData GetStageBinaryData()
        {
            string pattern = @"Resources/(.*)\.prefab";

            string mapPath = ExtractPath(AssetDatabase.GetAssetPath(MapPrefab), pattern);
            string PlayerPath = ExtractPath(AssetDatabase.GetAssetPath(PlayerPrefab), pattern);
            string PlayerNPCPath = ExtractPath(AssetDatabase.GetAssetPath(PlayerNPCPrefab), pattern);
            return new StageBinaryData(mapPath, PlayerPath, PlayerNPCPath, this.name);
        }

        public StageData SetStageData(StageBinaryData data)
        {
            MapPrefab = Resources.Load<GameObject>(data.MapPrefabPath);
            //MapPrefab = Resources.Load<GameObject>("Prefabs/Map/TestMap");
            PlayerPrefab = Resources.Load<GameObject>(data.PlayerPrefabPath);
            PlayerNPCPrefab = Resources.Load<GameObject>(data.PlayerNPCPrefabPath);
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
        public StageBinaryData(string mapPath,string PlayerPath,string PlayerNPCPath , string name)
        {
            MapPrefabPath = mapPath;
            PlayerPrefabPath = PlayerPath;
            PlayerNPCPrefabPath = PlayerNPCPath;
            StageName = name;
        }

        public string MapPrefabPath = "";
        public string PlayerPrefabPath = "";
        public string PlayerNPCPrefabPath = "";
        public string StageName = "";
    }
}