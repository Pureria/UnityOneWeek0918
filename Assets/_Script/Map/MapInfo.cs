using MorseGame.Enemy;
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
        [SerializeField] private FallGround _FallGround;
        [SerializeField] private EnemyManager _EnemyManager;
        [SerializeField] private bool _IsPlayerRight = false;

        public ObjectManager ObjectManager { get { return _ObjectManager; } }
        public Transform PlayerSpawnPosition { get { return _PlayerSpawnPosition; } }
        public Manager.GameManager GameManager { get { return _GameManager;  } }

        public GoalPoint GoalPoint { get { return _GoalPoint; } }

        public FallGround FallGround { get { return _FallGround; } }

        public EnemyManager EnemyManager { get { return _EnemyManager; } }

        public bool IsPlayerRight { get { return _IsPlayerRight; } }
    }
}
