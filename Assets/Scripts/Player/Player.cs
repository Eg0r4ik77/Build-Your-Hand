using System;
using Economy;
using Enemies;
using Movement;
using PlayerCamera;
using Skills;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerMovement), typeof(Animator))]
public class Player : MonoBehaviour, IUniversalHandOwner, IEnemyTarget, IAcceleratable, IPauseable
{
    [SerializeField] private float _skillApplyRange = 8f;
    [SerializeField] private float _attackRange = 6f;
    
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _maxHealth = 100f;
    
    [SerializeField] private FirstPersonCamera _camera;
    [SerializeField] private UniversalHand _hand;

    [SerializeField] private int _startWalletSum;
    
    private readonly int _punchAnimationHash = Animator.StringToHash("PlayerPunch");

    private float _health;

    private PlayerMovement _movement;
    private Animator _animator;
    private CapsuleCollider _capsuleCollider;

    public event Action<float> HealthChanged;
    public event Action Damaged;
    public event Action Died;

    public FirstPersonCamera Camera => _camera;
    public UniversalHand Hand => _hand;
    public ResourcesWallet Wallet { get; } = new();

    public Vector3 CenterPosition => transform.position;
    public CapsuleCollider Collider => _capsuleCollider;

    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            HealthChanged?.Invoke(_health / _maxHealth);
        }
    }

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        Health = _maxHealth;
        _hand.SetPlayer(this);
        Wallet.Add(_startWalletSum);
    }

    public void Move(Vector3 motion)
    {
        _movement.Move(motion);
    }

    public void TryApplyDamage(float damage)
    {
        Health -= damage;
        
        if (Health <= 0)
        {
            Die();
            return;
        }
        
        Damaged?.Invoke();
    }

    public void AddResource(Resource resource)
    {
        Wallet.Add(resource.Value);
    }
    
    public void TryAttack()
    {
        _animator.Play(_punchAnimationHash);
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

    private void OnAttackAnimation()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _attackRange))
        { 
            if (hit.transform.TryGetComponent(out IApplyableDamage applyableDamageTarget))
            {
                applyableDamageTarget.TryApplyDamage(_damage);
            }
        }
    }
    
    public void RotateHorizontally(float horizontalAxisRotation)
    {
        _movement.RotateHorizontally(horizontalAxisRotation);
    }

    public bool HasNoSkills()
    {
        return !_hand.Usable();
    }
    
    public void TryAccelerate(float acceleration)
    {
        _movement.Strategy = new AcceleratedMovement(acceleration);
    }

    public void ResetAcceleration()
    {
        _movement.Strategy = new SimpleMovement();
    }

    public void ResetHealth()
    {
        Health = _maxHealth;
    }
    
    public void SetPaused(bool paused)
    {
        _movement.enabled = !paused;
        _animator.enabled = !paused;
        _hand.SetPaused(paused);
    }
    
    private void Die()
    {
        Died?.Invoke();
    }
}