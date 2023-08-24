namespace Enemies
{
    public class BombEnemy : Enemy
    {
        private EnemyMovement _movement;
        private BombEnemyAttack _attack;

        protected override void CacheEnemyComponents()
        {
            base.CacheEnemyComponents();

            _movement = GetComponent<EnemyMovement>();
            _attack = GetComponent<BombEnemyAttack>();

            patrol = GetComponent<Patrol>();
        }

        protected override void InitializeEnemyComponents()
        {
            _movement.Initialize(this, navMeshAgent, animator);
            _attack.Initialize(this);
        }

        protected override void InitializeBehaviourTreeVariables()
        {
            SetBehaviourTreeVariable("Enemy",this);
            SetBehaviourTreeVariable("Movement", _movement);
            SetBehaviourTreeVariable("Patrol", patrol);
            SetBehaviourTreeVariable("Attack", _attack);
            SetBehaviourTreeVariable("AttackRange", _attack.Range);
            SetBehaviourTreeVariable("DelayBeforeAttackSeconds", _attack.DelayBeforeExplosion);
        }
    }
}