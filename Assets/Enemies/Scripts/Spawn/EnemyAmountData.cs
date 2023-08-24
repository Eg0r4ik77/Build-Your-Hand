using System;
using UnityEngine;

namespace Enemies.Spawn
{
    [Serializable]
    public class EnemyAmountData
    {
        [field:SerializeField] public EnemyType Type { get; private set; }
        [field:SerializeField] public int Amount { get; private set; }
    }
}