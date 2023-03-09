using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies.Spawn
{
    [Serializable]
    public class RandomSpawnInfo
    {
        [SerializeField] private List<EnemySpawnInfo> _enemySpawnInfos;
        [SerializeField] private List<Transform> _points;

        public List<EnemySpawnInfo> EnemySpawnInfos => _enemySpawnInfos;
        public List<Transform> Points => _points;

        public void RandomizePoints()
        {
            var random = new System.Random();
            _points = _points.OrderBy(point => random.Next()).ToList();
        }
    }
}