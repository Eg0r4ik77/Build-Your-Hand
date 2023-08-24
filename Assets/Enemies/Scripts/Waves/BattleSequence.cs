using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Enemies.Spawn;
using Enemies.Waves;
using UnityEngine;
using Zenject;

public class BattleSequence : MonoBehaviour
{
    [SerializeField] private List<EnemyWaveData> _waves;
    [SerializeField] private float _signalDistance = 6f;
    [SerializeField] private List<GameObject> _obstacles;

    private List<IEnemyTarget> _enemyTargets;
    private EnemyTargetDetector _targetDetector;
    private EnemySpawner _enemySpawner;
    
    private List<Enemy> _currentEnemies;
    
    private bool _initialized;
    
    private int _currentWaveIndex;
    private int _currentWaveEnemiesAmount;
    
    [Inject]
    private void Construct(List<IEnemyTarget> targets, EnemyTargetDetector targetDetector, EnemySpawner enemySpawner)
    {
        _enemyTargets = targets;
        _targetDetector = targetDetector;
        _enemySpawner = enemySpawner;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEnemyTarget _) && !_initialized)
        {
            InitializeSequence();
        }
    }

    private void InitializeSequence()
    {
        _initialized = true;
        
        SubscribeSequenceCleaningToTargetsDeath();
        SetObstacles(true);
        SpawnWave();
    }
    
    private void UpdateSequenceScenario(Enemy deadEnemy)
    {
        // there was a check for initialized == false
        
        _currentWaveEnemiesAmount--;

        if (!WaveIsOver())
        {
            return;
        }
        
        UnsubscribeScenarioFromWave();
        _currentWaveIndex++;

        if (SequenceIsOver())
        {
            FinishSequence();
        }
        else
        {
            StartNextWave();
        }
    }

    protected virtual void StartNextWave()
    {
        SpawnWave();
        _targetDetector.DetectAll();
    }
    
    protected virtual void FinishSequence()
    {
        ResetSequence();
        SetObstacles(false);
    }

    protected void ResetCurrentWave()
    {
        _currentWaveIndex = 0;
        _currentWaveEnemiesAmount = 0;
    }

    private void SpawnWave()
    {
        EnemyWaveData info = _waves[_currentWaveIndex];
        List<EnemySpawnData> pointInfos = info.SpawnDatas;
        var enemies = new List<Enemy>() ;

        foreach (EnemySpawnData enemySpawnData in pointInfos)
        {
            List<Enemy> spawnedEnemies = SpawnWaveBySpawnData(enemySpawnData);
            enemies.AddRange(spawnedEnemies);
            
            _currentWaveEnemiesAmount += enemySpawnData.Amount;
        }

        SubscribeScenarioToEnemies(enemies);
        _currentEnemies = enemies;
    }

    private List<Enemy> SpawnWaveBySpawnData(EnemySpawnData enemySpawnData)
    {
        EnemyType type = enemySpawnData.Type;
        Vector3 position = enemySpawnData.Point.position;
        int amount = enemySpawnData.Amount;

        List<Enemy> enemies = _enemySpawner.SpawnMany(type, position, amount);

        return enemies;
    }

    private void ResetSequence()
    {
        ResetCurrentWave();

        _targetDetector.UndetectAll();
        UnsubscribeSequenceCleaningFromTargetsDeath();
    }
    
    private void SetObstacles(bool state)
    {
        foreach (GameObject obstacle in _obstacles.Where(obstacle => obstacle))
        {
            obstacle.SetActive(state);             
        }
    }

    private void SubscribeSequenceCleaningToTargetsDeath()
    {
        foreach (IEnemyTarget target in _enemyTargets)
        {
            target.Died += CleanSequence;
        }
    }
    
    private void UnsubscribeSequenceCleaningFromTargetsDeath()
    {
        foreach (IEnemyTarget target in _enemyTargets)
        {
            target.Died -= CleanSequence;
        }
    }
    
    private void CleanSequence()
    {
        UnsubscribeScenarioFromWave();
        ReturnEnemiesToPool();
        ResetSequence();
        
        _currentEnemies = null;
        _initialized = false;
    }
    
    private void SubscribeScenarioToEnemies(List<Enemy> enemies)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.ReturnedToPool += UpdateSequenceScenario;
            enemy.DamagedByEnemyTarget += _targetDetector.Detect;
            
            _targetDetector.Detected += SendDetectingSignal;
        }
    }
    
    private void UnsubscribeScenarioFromWave()
    {
        foreach (Enemy enemy in _currentEnemies)
        {
            enemy.ReturnedToPool -= UpdateSequenceScenario;
            enemy.DamagedByEnemyTarget -= _targetDetector.Detect;
            
            _targetDetector.Detected -= SendDetectingSignal;
        }
    }

    private void ReturnEnemiesToPool()
    {
        foreach (Enemy enemy in _currentEnemies)
        {
            enemy.ReturnToPool();
        }
    }

    private void SendDetectingSignal(Enemy enemy)
    {
        _targetDetector.SendSignal(enemy, _currentEnemies, _signalDistance);
    }
    
    private bool WaveIsOver()
    {
        return _currentWaveEnemiesAmount <= 0;
    }
    
    private bool SequenceIsOver()
    {
        return _currentWaveIndex == _waves.Count;
    }
}