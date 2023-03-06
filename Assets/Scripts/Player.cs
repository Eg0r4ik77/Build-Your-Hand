using System.Collections.Generic;
using System.Linq;
using Skills;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _skillApplyRange = 2f;
    [SerializeField] private float _health = 100f;
    
    [SerializeField] private Camera _camera;
    
    private PlayerMovement _movement;
    private List<Skill> _skills; 

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _skills = new List<Skill> { Skill.Hacking };
    }

    public void Move(Vector3 motion)
    {
        _movement.Move(motion);
    }

    public void Attack(IApplyableDamage target)
    {
        target.TryApplyDamage(2f);
    }
    
    public Skill TryGetSkill(Skill skill) 
    {
        return _skills.FirstOrDefault(s => s == skill);
    }
    
    public void AddSkill(Skill skill)
    {
        _skills.Add(skill);
    }

    public bool TryUseSkill(ISkillTarget target, Skill skill)
    {
        return target.TryApplySkill(skill);
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

    public void TryApplyDamage(float damage)
    {
        _health -= damage;

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
}