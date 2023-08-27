using Enemies;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    private static readonly int DieHash = Animator.StringToHash("Die");

    private EnemyMovement _movement;
    private MeleeEnemyAttack _attack;

    protected override void CacheEnemyComponents()
    {
        base.CacheEnemyComponents();

        _movement = GetComponent<EnemyMovement>();
        _attack = GetComponent<MeleeEnemyAttack>();

        reaction = GetComponent<Reaction>();
        patrol = GetComponent<Patrol>();
    }

    protected override void InitializeEnemyComponents()
    {
        _movement.Initialize(this, navMeshAgent, animator);
        _attack.Initialize(this, animator);
        
        reaction.Initialize(animator, behaviourTreeOwner);
    }

    protected override void InitializeBehaviourTreeVariables()
    {
        SetBehaviourTreeVariable("Enemy",this);
        SetBehaviourTreeVariable("Movement", _movement);        
        SetBehaviourTreeVariable("Patrol", patrol);        
        SetBehaviourTreeVariable("Attack", _attack);
        SetBehaviourTreeVariable("AttackRange", _attack.Range);
    }
    
    protected override void Die()
    { 
        CanApplyDamage = false;
        behaviourTreeOwner.enabled = false;
        AnimateDying(); 
    }

    private void AnimateDying()
    {
        animator.SetTrigger(DieHash);
    }
    
    private void OnDyingAnimation()
    {
        base.Die();
    }
}