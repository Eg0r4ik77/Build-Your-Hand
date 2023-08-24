using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Factory")]
public class EnemyPrefabs : ScriptableObject
{
    [SerializeField] private List<Enemy> _enemyPrefabs;

    public T Get<T>() where T : Enemy
    {
        var enemyPrefab = (T)_enemyPrefabs.FirstOrDefault(enemy => enemy is T);
        return enemyPrefab;
    }
}
