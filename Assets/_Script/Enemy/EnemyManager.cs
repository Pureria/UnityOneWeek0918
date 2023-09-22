using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MorseGame.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private List<EnemyStateController> _Enemyes = new List<EnemyStateController>();

        public void AddEnemy(EnemyStateController enemy)
        {
            if (!_Enemyes.Contains(enemy)) _Enemyes.Add(enemy);
        }

        public void GameStart()
        {
            foreach(EnemyStateController enemy in _Enemyes)
            {
                enemy.StartGame();
            }
        }

        public void GameEnd()
        {
            foreach (EnemyStateController enemy in _Enemyes)
            {
                enemy.EndGame();
            }
        }
    }
}