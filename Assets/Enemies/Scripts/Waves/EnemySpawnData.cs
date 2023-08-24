using System;
using Enemies.Spawn;
using UnityEngine;

namespace Enemies.Waves
{
    [Serializable]
    public class EnemySpawnData
    {
        [field: SerializeField] public Transform Point { get; private set; }
        [field:SerializeField] public EnemyType Type { get; private set; }
        [field:SerializeField] public int Amount { get; private set; }
    }
}