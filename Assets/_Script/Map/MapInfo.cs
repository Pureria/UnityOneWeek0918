using MorseGame.Object.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Map
{
    public class MapInfo : MonoBehaviour
    {
        [SerializeField] private ObjectManager _ObjectManager;
        [SerializeField] private Transform _PlayerSpawnPosition;
        [SerializeField] private Manager.GameManager _GameManager;
        [SerializeField] private GoalPoint _GoalPoint;

        public ObjectManager ObjectManager { get { return _ObjectManager; } }
        public Transform PlayerSpawnPosition { get { return _PlayerSpawnPosition; } }
        public Manager.GameManager GameManager { get { return _GameManager;  } }

        public GoalPoint GoalPoint { get { return _GoalPoint; } }
    }
}
