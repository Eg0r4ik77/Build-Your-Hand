using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemyTargetDetector : MonoBehaviour
    {
        private List<Enemy> _enemies;
        private List<IEnemyTarget> _targets;
        
        [Inject]
        public void Construct(List<Enemy> enemies, List<IEnemyTarget> targets)
        {
            _enemies = enemies;
            _targets = targets;
        }

        // 0.1с
        private void Update()
        {
            if(_enemies != null)
                TryDetect();
        }

        public void SetEnemies(List<Enemy> enemies)
        {
            _enemies = enemies;
        }

        private void TryDetect()
        {
            foreach (Enemy enemy in _enemies)
            {
                foreach (IEnemyTarget target in _targets)
                {
                    if (target != null && enemy != null && enemy.TryDetect(target))
                    {
                        DetectAll();
                        break;
                    }
                }
            }
        }

        public void DetectAll()
        {
            foreach (Enemy enemy in _enemies)
            {
                IEnemyTarget target = FindClosestTargetTo(enemy);
                Detect(enemy, target);
            }
        }
        
        public void UndetectAll()
        {
            foreach (IEnemyTarget enemyTarget in _targets)
            {
                foreach (Enemy enemy in _enemies)
                {
                    Undetect(enemy, enemyTarget);
                }
            }
        }

        private IEnemyTarget FindClosestTargetTo(Enemy enemy)
        {
            IEnemyTarget closestTarget = _targets[0];
            float minDistance = Vector3.Distance(closestTarget.CenterPosition, enemy.Center.position);


            foreach (IEnemyTarget target in _targets)
            {
                float distance = Vector3.Distance(target.CenterPosition, enemy.Center.position);
                if (distance < minDistance)
                {
                    closestTarget = target;
                    minDistance = distance;
                }
            }

            return closestTarget;
        }
        
        private void Detect(Enemy enemy, IEnemyTarget target)
        {
            enemy.Target = target;
        }
        
        private void Undetect(Enemy enemy, IEnemyTarget target)
        {
            enemy.Target = null;
        }
    }
}