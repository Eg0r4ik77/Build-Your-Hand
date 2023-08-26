using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeleeEnemyAttack : EnemyAttack
{
    [SerializeField] private MeleeEnemyAttackConfig _config;
    
    private static readonly int PunchHash = Animator.StringToHash("Punch");
    private static readonly int PunchingHash = Animator.StringToHash("Punching");

    private static readonly int KickHash = Animator.StringToHash("Kick");
    private static readonly int KickingHash = Animator.StringToHash("Kicking");
    
    private int _currentAnimationHash = PunchingHash;

    private MeleeEnemy _enemy;
    private Animator _animator;
    
    private float _damage;
    private float _range;
    private float _cooldown;
    private float _punchProbability;

    public float Range => _range;
    public float Cooldown => _cooldown;
    
    private void Update()
    {
        Finished = IsAnimationFinished();
    }

    public void Initialize(MeleeEnemy enemy, Animator animator)
    {
        _enemy = enemy;
        _animator = animator;
        
        SetConfigValues();
    }
    
    public void Punch()
    {
        Finished = false;
        AnimateAttack();
    }
    
    public void Stop()
    {
        _animator.SetBool(_currentAnimationHash, false);
    }

    protected override void SetConfigValues()
    {
        _damage = _config.Damage;
        _range = _config.Range;
        _cooldown = _config.Cooldown;
        _punchProbability = _config.PunchProbability;
    }

    private void OnAttackAnimation()
    {
        IEnemyTarget target = _enemy.Target;

        if (CheckAttackConditions(_enemy, _range))
        {
            target.TryApplyDamage(_damage);
        }
    }
    
    private void AnimateAttack()
    {
        _animator.SetBool(_currentAnimationHash, false);
        SetRandomAttackAnimation();
        _animator.SetBool(_currentAnimationHash, true);
    }
    
    private void SetRandomAttackAnimation()
    {
        float randomAttackProbability = Random.Range(0, 1f);

        _currentAnimationHash = randomAttackProbability <= _punchProbability
            ? PunchingHash
            : KickingHash;
    }

    private bool IsAnimationFinished()
    {
        AnimatorStateInfo currentAnimatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        int attackHash = currentAnimatorStateInfo.shortNameHash;

        bool isPunchState = attackHash == PunchHash;
        bool isUppercutState = attackHash == KickHash;

        bool isAnimationFinished = !isPunchState && !isUppercutState;
        
        return isAnimationFinished;
    }
}
