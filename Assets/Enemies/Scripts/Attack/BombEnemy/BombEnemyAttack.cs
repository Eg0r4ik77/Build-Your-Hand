using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Zenject;

public class BombEnemyAttack : EnemyAttack
{
    [SerializeField] private BombEnemyAttackConfig _config;
    
    private float _range;
    private BombExplosion _explosion;

    private BombEnemy _enemy;
    private Animator _animator;
    
    private List<IEnemyTarget> _targets;

    public float Range => _range;

    [Inject]
    private void Construct(List<IEnemyTarget> targets)
    {
        _targets = targets;
    }

    public void Initialize(BombEnemy enemy, Animator animator)
    {
        _enemy = enemy;
        _animator = animator;
        
        SetConfigValues();
    }

    public void Attack()
    {
        _animator.SetTrigger("Jump");
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
        _range = _config.Range;
        _explosion = _config.Explosion;
    }
}
