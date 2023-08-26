using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class EnemyTargetDetector : MonoBehaviour
    {
        private List<Enemy> _enemies;
        private List<IEnemyTarget> _targets;

        public Action<Enemy> Detected;
        
        [Inject]
        public void Construct(List<Enemy> enemies, List<IEnemyTarget> targets)
        {
            _enemies = enemies;
            _targets = targets;
        }
        
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
                        Detect(enemy, target);
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
            foreach (Enemy enemy in _enemies)
            {
                Undetect(enemy);
            }
        }
        
        public void Detect(Enemy enemy, IEnemyTarget target)
        {
            enemy.Target = target;
            Detected?.Invoke(enemy);
        }
        
        public void SendSignal(Enemy sourceEnemy, List<Enemy> enemies, float signalDistance)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != sourceEnemy && enemy.InUse && enemy.Target == null)
                {
                    float distance = Vector3.Distance(sourceEnemy.transform.position, enemy.transform.position);

                    if (distance < signalDistance)
                    {
                        Detect(enemy, sourceEnemy.Target);
                    }
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

        private void Undetect(Enemy enemy)
        {
            enemy.Target = null;
        }
    }
}