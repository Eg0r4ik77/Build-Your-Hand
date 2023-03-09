using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Spawn
{
    [Serializable]
    public class EnemyWaveInfo
    {
        [SerializeField] private RandomSpawnInfo _randomSpawnInfo;
        
        public RandomSpawnInfo RandomSpawnInfo => _randomSpawnInfo;
    }
}