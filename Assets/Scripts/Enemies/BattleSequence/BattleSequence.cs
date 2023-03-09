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
        [SerializeField] private BattleSequenceTimer _timer;
        
        private bool _initialized;
        private int _currentWaveIndex;
        private int _currentWaveEnemiesCount;

        private List<Enemy> _currentEnemies;

        private Player _target;

        private void Awake()
        {
            _target = FindObjectOfType<Player>();
        }

        private void Start()
        {
            _timer.Finished += InitializeSequence;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player _) && !_initialized)
            {
                _target.ResetHealth();
                TrySetObstacles(true);
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
            _currentWaveIndex = _currentWaveEnemiesCount = 0;
            _timer.StartTimer();
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
                _timer.StartTimer();
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
            InitializeEnemies();
            
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
            
            _currentEnemies = _enemySpawner.SpawnWave(spawnInfo);
        }

        private void InitializeEnemies()
        {
            foreach (Enemy enemy in _currentEnemies)
            {
                enemy.Target = _target;
                enemy.TryDetectTarget();
                enemy.Died += UpdateSequenceScenario;
            }
        }

        private void TrySetObstacles(bool state)
        {
            foreach (var obstacle in _obstacles.Where(obstacle => obstacle))
            {
                 obstacle.SetActive(state);
            }
        }
    }
}