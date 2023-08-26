using System.Collections.Generic;
using Enemies.Spawn;
using ScriptableObjects.Enemies;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemyPoolsProvider : IInitializable
    {
        private readonly Transform _root;
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemyAmounts _enemyAmountsData;
        private readonly EnemyTargetDetector _targetDetector;
        
        private readonly Dictionary<EnemyType, EnemyPool> _enemyPools = new();

        [Inject]
        public EnemyPoolsProvider(Transform root,
            EnemyFactory enemyFactory,
            EnemyAmounts enemyAmountsData,
            EnemyTargetDetector targetDetector)
        {
            _root = root;
            _enemyFactory = enemyFactory;
            _enemyAmountsData = enemyAmountsData;
            _targetDetector = targetDetector;
        }

        public EnemyPool Get(EnemyType type) => _enemyPools[type];
        
        public void Initialize()
        {
            InitializePools();
            _targetDetector.SetEnemies(_enemyFactory.CreatedEnemies);
        }

        private void InitializePools()
        {
            foreach (EnemyAmountData data in _enemyAmountsData.Data)
            {
                EnemyType type = data.Type;
                int amount = data.Amount;

                EnemyPool pool = new EnemyPool(type, amount, _root, _enemyFactory);
                pool.Initialize();

                _enemyPools[type] = pool;
            }
        }
    }
}