using Enemies.Pool;
using UnityEngine;

namespace Enemies.Spawn
{
    public class EnemyPool : MonoBehaviourObjectsPool
    {
        private readonly EnemyType _type;
        private readonly EnemyFactory _enemyFactory;

        public EnemyPool(EnemyType type, int size, Transform rootTransform, EnemyFactory enemyFactory) 
            : base(size, rootTransform)
        {
            _type = type;
            _enemyFactory = enemyFactory;
        }

        protected override Pool.IPoolObject Create()
        {
            Enemy enemy = _enemyFactory.Create(_type);

            enemy.ReturnedToPool += Release;
            
            AttachToRoot(enemy.transform);
            Release(enemy);

            return enemy;
        }
    }
}