using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Enemies/Factory")]
    public class EnemyFactory : ScriptableObject
    {
        [SerializeField] private Enemy _simpleEnemyPrefab;
        [SerializeField] private Enemy _fastEnemyPrefab;

        public Enemy Get(EnemyType type)
        {
            return type switch
            {
                EnemyType.Simple => _simpleEnemyPrefab,
                EnemyType.Fast => _fastEnemyPrefab,
                _ => null
            };
        }
    }
}