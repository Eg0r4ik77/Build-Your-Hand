using System;
using System.Collections;
using Economy;
using Skills;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Animator))]
public class Player : MonoBehaviour, IAcceleratable
{
    [SerializeField] private float _skillApplyRange = 8f;
    [SerializeField] private float _attackRange = 6f;
    
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _maxHealth = 100f;
    
    [SerializeField] private Camera _camera;
    [SerializeField] private UniversalHand _hand;
    
    private PlayerMovement _movement;
    private Animator _animator;

    private float _health;
    
    private readonly int _punchAnimationHash = Animator.StringToHash("PlayerPunch");

    public UniversalHand Hand => _hand;
    public ResourcesWallet Wallet { get; } = new();

    public event Action<float> Damaged;
    public event Action Died;
    
    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        
        _health = _maxHealth;
    }

    private void Start()
    {
        Wallet.Add(100f);
        _hand.SetPlayer(this);
    }

    public void Move(Vector3 motion)
    {
        _movement.Move(motion);
    }

    public void TryApplyDamage(float damage)
    {
        _health -= damage;
        Damaged?.Invoke(_health / _maxHealth);
        
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Died?.Invoke();
    }

    public void AddResource(Resource resource)
    {
        Wallet.Add(resource.Value);
    }
    
    public void TryAttack()
    {
        _animator.Play(_punchAnimationHash);
        
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _attackRange))
        {
            if (hit.transform.TryGetComponent(out IApplyableDamage applyableDamageTarget))
            {
                StartCoroutine(AttackCoroutine(applyableDamageTarget));
            }
        }
    }

    public T TryGetTarget<T>()
    {
        T target = default;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _skillApplyRange))
        {
            target = hit.transform.GetComponent<T>();
        }
            
        return target;
    }

    private IEnumerator AttackCoroutine(IApplyableDamage target)
    {
        const float attackAnimationTimeInSeconds = .15f;
        yield return new WaitForSeconds(attackAnimationTimeInSeconds);
        target.TryApplyDamage(_damage);
    }
    
    public void RotateHorizontally(float horizontalAxisRotation)
    {
        _movement.RotateHorizontally(horizontalAxisRotation);
    }
    
    public bool HasNoSkills()
    {
        return !_hand.Usable();
    }

    public bool TryAccelerate()
    {
        throw new NotImplementedException();
    }
}