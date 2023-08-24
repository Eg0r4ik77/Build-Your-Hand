using System.Collections.Generic;
using Enemies;
using Enemies.Spawn;
using Zenject;

public class EnemyFactory
{
    private readonly DiContainer _diContainer;
    private readonly EnemyPrefabs _enemyPrefabs;
    
    public List<Enemy> CreatedEnemies { get; } = new();

    public EnemyFactory(DiContainer diContainer, EnemyPrefabs prefabs)
    {
        _diContainer = diContainer;
        _enemyPrefabs = prefabs;
    }
    
    public Enemy Create(EnemyType type)
    {
        Enemy enemyPrefab = Get(type);
        
        var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab);
        enemy.Initialize();
        
        CreatedEnemies.Add(enemy);
        return enemy;
    }

    private Enemy Get(EnemyType type)
    {
        return type switch
        {
            EnemyType.Melee => _enemyPrefabs.Get<MeleeEnemy>(),
            EnemyType.Bomb => _enemyPrefabs.Get<BombEnemy>(),
            _ => null
        };
    }       
}