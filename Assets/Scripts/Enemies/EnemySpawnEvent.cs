using System.Collections.Generic;
using Enemies.Spawn;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawnEvent : MonoBehaviour
    {
        [SerializeField] private RandomSpawnInfo _info;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private float _signalRange = 6;
        
        private List<Enemy> _enemies;
        private IEnemyTarget _target;

        private bool _isInitialized;
        
        private void Awake()
        {
            _target = FindObjectOfType<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;
            TrySpawnEnemiesInRandomPoints();
            InitializeEnemies();
        }
        
        private void TrySpawnEnemiesInRandomPoints()
        {
            RandomSpawnInfo spawnInfo = _info;

            spawnInfo.RandomizePoints();
            
            _enemies = _enemySpawner.SpawnWave(spawnInfo);
        }

        private void InitializeEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Target = _target;
                enemy.Detected += SendDetectingSignal;
            }
        }

        private void SendDetectingSignal(Enemy signalingEnemy)
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy != signalingEnemy && !enemy.IsDead() && !enemy.DetectedTarget)
                {
                    float distance = Vector3.Distance(signalingEnemy.transform.position, enemy.transform.position);

                    if (distance < _signalRange)
                    {
                        enemy.TryDetectTarget();
                    }
                }
            }            
        }
    }
}