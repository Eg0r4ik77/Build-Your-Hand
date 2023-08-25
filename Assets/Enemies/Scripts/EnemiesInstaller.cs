using System.Collections.Generic;
using Enemies.Spawn;
using ScriptableObjects.Enemies;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private Transform _root;
        [SerializeField] private EnemyPrefabs _prefabs;
        [SerializeField] private EnemyAmounts _enemyAmountsData;
        [SerializeField] private EnemyTargetDetector _targetDetector;

        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
        
        public override void InstallBindings()
        {
            var targets = new List<IEnemyTarget>
            { 
                _player
            };
            
            Container
                .Bind<List<IEnemyTarget>>()
                .FromInstance(targets)
                .AsSingle();
            
            Container
                .Bind<List<Enemy>>()
                .AsSingle();

            Container
                .Bind<EnemyTargetDetector>()
                .FromInstance(_targetDetector)
                .AsSingle();

            Container
                .Bind<EnemyFactory>()
                .AsSingle()
                .WithArguments(_prefabs);

            Container
                .Bind<EnemyAmounts>()
                .FromInstance(_enemyAmountsData)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<EnemyPoolsProvider>()
                .AsSingle()
                .WithArguments(_root);

            Container
                .Bind<EnemySpawner>()
                .AsSingle();
        }
    }
}