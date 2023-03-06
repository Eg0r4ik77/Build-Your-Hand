using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    private PlayerMovement _movement;
    private List<Skill> _skills; 

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _skills = new List<Skill>();
        _skills.Add(Skill.Hacking);
    }

    public void Move(Vector3 motion)
    {
        _movement.Move(motion);
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
}