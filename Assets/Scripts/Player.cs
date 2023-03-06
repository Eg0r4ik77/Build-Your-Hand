using System;
using System.Collections.Generic;
using System.Linq;
using Skills;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _skillApplyRange = 2f;
    [SerializeField] private float _damage = 2f;
    [SerializeField] private float _health = 100f;
    
    [SerializeField] private Camera _camera;
    
    private PlayerMovement _movement;
    private List<Skill> _skills;
    
    private float _maxHealth;

    public event Action<float> Damaged;
    
    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _maxHealth = _health;
        _skills = new List<Skill> { new Hacking(), new Shooting(100f) };
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

    public void AddSkill(Skill skill)
    {
        _skills.Add(skill);
    }

    public bool TryUseSkill<T0, T1>() 
        where T0 : ISkillTarget
        where T1 : Skill
    {
        T0 target = TryGetTarget<T0>();
        T1 skill = TryGetSkill<T1>();
        
        if (target != null && skill != null)
        {
            return skill.TryActivate(target);
        }

        return false;
    }

    private T TryGetTarget<T>()
    {
        T target = default;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _skillApplyRange))
        {
            target = hit.transform.GetComponent<T>();
        }
            
        return target;
    }
    
    private T TryGetSkill<T>() where T : Skill
    {
        return _skills.FirstOrDefault(s => s is T) as T;
    }
}