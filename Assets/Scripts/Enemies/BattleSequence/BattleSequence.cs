using System.Collections.Generic;
using System.Linq;
using Enemies.Spawn;
using UnityEngine;

namespace Enemies.BattleSequence
{
    public class BattleSequence : MonoBehaviour, IBattleSequence
    {
        [SerializeField] private List<EnemyWaveInfo> _waves;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private List<GameObject> _obstacles;

        private bool _initialized;
        private int _currentWaveIndex;
        private int _currentWaveEnemiesCount;

        private IEnemyTarget _target;

        private void Awake()
        {
            _target = FindObjectOfType<Player>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player _) && !_initialized)
            { 
                InitializeSequence();                
            }
        }
        
        
        public void InitializeSequence()
        {
            _initialized = true;

            SpawnWave();
        }

        public void FinishSequence()
        {
            // door opened
        }
        
        public void UpdateSequenceScenario()
        {
            _currentWaveEnemiesCount--;

            if (_currentWaveEnemiesCount != 0)
            {
                return;                
            }
            
            if (_currentWaveIndex != _waves.Count)
            {
                SpawnWave();                
            }
            else
            {
                FinishSequence();                
            }
        }

        private void SpawnWave()
        {
            EnemyWaveInfo info = _waves[_currentWaveIndex];

            TrySpawnEnemiesInRandomPoints(info);
            
            _currentWaveIndex++;
        }

        private void TrySpawnEnemiesInRandomPoints(EnemyWaveInfo info)
        {
            RandomSpawnInfo spawnInfo = info.RandomSpawnInfo;

            foreach (EnemySpawnInfo enemySpawnInfo in spawnInfo.EnemySpawnInfos)
            {
                _currentWaveEnemiesCount += enemySpawnInfo.Count;
            }
            
            spawnInfo.RandomizePoints();
            
            var enemies = _enemySpawner.SpawnWave(spawnInfo);
            InitializeEnemies(enemies);
        }

        private void InitializeEnemies(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                // enemy set target;
                enemy.Died += UpdateSequenceScenario;
                
                // enemy damaged => detected (???) 
                // detected += TrySetObstacles
            }
        }

        private void TrySetObstacles(bool state)
        {
            foreach (var obstacle in _obstacles.Where(obstacle => obstacle))
                obstacle.SetActive(state);
        }
    }
}