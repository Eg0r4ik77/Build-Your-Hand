using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Zenject;

public class BombEnemyAttack : EnemyAttack
{
    [SerializeField] private BombEnemyAttackConfig _config;
    
    private float _range;
    private float _delayBeforeExplosion;
    private BombExplosion _explosion;

    private BombEnemy _enemy;
    private List<IEnemyTarget> _targets;

    public float Range => _range;
    public float DelayBeforeExplosion => _delayBeforeExplosion;

    [Inject]
    private void Construct(List<IEnemyTarget> targets)
    {
        _targets = targets;
    }

    public void Initialize(BombEnemy enemy)
    {
        _enemy = enemy;
        SetConfigValues();
    }
    
    public void Explode()
    {
        BombExplosion explosion = Instantiate(_explosion, _enemy.Center.position, Quaternion.identity);

        var layerMask = 1 << LayerMask.NameToLayer("Player");

        explosion.Initialize(_targets, layerMask);
        explosion.Execute();
        
        _enemy.ReturnToPool();
        Finished = true;
    }
    
    protected override void SetConfigValues()
    {
        _range = _config.DelayBeforeExplosion;
        _delayBeforeExplosion = _config.DelayBeforeExplosion;
        _explosion = _config.Explosion;
    }
}
