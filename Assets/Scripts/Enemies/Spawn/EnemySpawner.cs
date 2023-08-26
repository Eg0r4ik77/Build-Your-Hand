using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Enemies.Spawn
{
    public class EnemySpawner
    {
        private readonly EnemyPoolsProvider _enemyPoolsProvider;

        [Inject]
        public EnemySpawner(EnemyPoolsProvider enemyPoolsProvider)
        {
            _enemyPoolsProvider = enemyPoolsProvider;
        }

        public List<Enemy> SpawnMany(EnemyType type, Vector3 position, int amount)
        {
            var enemies = new List<Enemy>();
            
            for (int i = 0; i < amount; i++)
            {
                Enemy enemy = Spawn(type, position);
                enemies.Add(enemy);
            }

            return enemies;
        }

        private Enemy Spawn(EnemyType type, Vector3 position)
        {
            EnemyPool pool = _enemyPoolsProvider.Get(type);
            Enemy enemy = (Enemy)pool.Get();

            enemy.transform.position = position;
            enemy.gameObject.SetActive(true);

            return enemy;
        }
    }
}