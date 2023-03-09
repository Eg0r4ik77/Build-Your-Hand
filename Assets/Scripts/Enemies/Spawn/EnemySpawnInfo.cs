using System;
using System.Xml.Linq;
using UnityEngine;

namespace Enemies.Spawn
{
    [Serializable]
    public class EnemySpawnInfo
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private int _count;

        public EnemyType Type => _type;
        public int Count => _count;

        public EnemySpawnInfo(EnemyType type, int count)
        {
            _type = type;
            _count = count;
        }
    }
}