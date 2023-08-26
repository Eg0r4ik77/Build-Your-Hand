using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Waves
{
    [Serializable]
    public class EnemyWaveData
    {
        [field: SerializeField] public List<EnemySpawnData> SpawnDatas;
    }
}