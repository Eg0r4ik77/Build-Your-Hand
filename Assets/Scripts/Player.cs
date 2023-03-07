using System;
using Economy;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _skillApplyRange = 2f;
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _maxHealth = 100f;
    
    [SerializeField] private Camera _camera;
    [SerializeField] private UniversalHand _hand;
    
    private PlayerMovement _movement;

    private float _health;

    public UniversalHand Hand => _hand;
    public ResourcesWallet Wallet { get; } = new();

    public event Action<float> Damaged;
    
    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _health = _maxHealth;
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
        
        print(_health);
        
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("Die");
    }

    public void AddResource(Resource resource)
    {
        Wallet.Add(resource.Value);
    }
    
    public void TryAttack()
    {
        float attackRange = _skillApplyRange;
        
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, attackRange))
        {
            if (hit.transform.TryGetComponent(out IApplyableDamage applyableDamageTarget))
            {
                applyableDamageTarget.TryApplyDamage(_damage);
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

    public bool HasNoSkills()
    {
        return !_hand.Usable();
    }
}