using System.Collections.Generic;
using Enemies.Spawn;
using UnityEngine;

namespace ScriptableObjects.Enemies
{
    [CreateAssetMenu(menuName = "Enemies/Amounts")]
    public class EnemyAmounts : ScriptableObject
    {
        [SerializeField] private List<EnemyAmountData> _enemyAmountsData;

        public List<EnemyAmountData> Data => _enemyAmountsData;
    }
}