using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;

        public List<Enemy> SpawnWave(RandomSpawnInfo spawnInfo)
        {
            int index = 0;
            var enemies = new List<Enemy>();

            foreach (EnemySpawnInfo info in spawnInfo.EnemySpawnInfos)
            {
                for (int i = 0; i < info.Count; i++)
                {
                    enemies.Add(Spawn(info.Type, spawnInfo.Points[index].position));
                    index = (index + 1) % spawnInfo.Points.Count;
                }
            }

            return enemies;
        }

        public List<Enemy> SpawnWave(EnemySpawnInfo spawnInfo, Vector3 position)
        {
            var enemies = new List<Enemy>();
            
            for (int i = 0; i < spawnInfo.Count; i++)
            {
                Enemy enemy = Spawn(spawnInfo.Type, position);
                enemies.Add(enemy);
            }
            
            return enemies;
        }

        private Enemy Spawn(EnemyType type, Vector3 position)
        {
            Enemy enemy = _enemyFactory.Get(type);
            enemy.transform.position = position;
            
            return enemy;
        }
    }
}