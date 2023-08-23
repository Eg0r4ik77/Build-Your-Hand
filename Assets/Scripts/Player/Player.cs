using System;
using System.Collections;
using Economy;
using Enemies;
using Movement;
using PlayerCamera;
using Skills;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Animator))]
public class Player : MonoBehaviour, IUniversalHandOwner, IEnemyTarget, IAcceleratable
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

    public FirstPersonCamera Camera => _camera;
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
        _hand.SetPlayer(this);
        Wallet.Add(_startWalletSum);
    }

    private void OnEnable()
    {
        Pause.Instance.OnPaused += SetPaused;
    }

    private void OnDisable()
    {
        Pause.Instance.OnPaused -= SetPaused;
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
        _health = _maxHealth;
    }

    private void SetPaused(bool paused)
    {
        _movement.enabled = !paused;
        _animator.enabled = !paused;
        _hand.SetPaused(paused);
    }
}