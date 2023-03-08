using System;
using System.Collections.Generic;
using System.Linq;
using Skills;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UniversalHand : MonoBehaviour
{
    [SerializeField] private List<Color> _skillColors;
    [SerializeField] private ParticleSystem _shootParticles;
    
    private IUniversalHandOwner _owner;
    
    private List<Skill> _skills;
    private Color _defaultColor;
    
    private MeshRenderer _meshRenderer;
    
    private int _currentSkillIndex;

    public Skill CurrentSkill { get; private set; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _skills = new List<Skill>();
        _defaultColor = _meshRenderer.material.color;
    }

    public void SetPlayer(IUniversalHandOwner owner)
    {
        _owner = owner;
    }
    
    public void SwitchSkill(float mouseScroll)
    {
        if (!Usable())
        {
            return;
        }
        
        int sign = Math.Sign(mouseScroll);

        _currentSkillIndex = _skills.Count == 1 
            ? 0 
            : (_currentSkillIndex + sign) % _skills.Count + (_currentSkillIndex + sign >= 0
                                 ? 0
                                 : _skills.Count);
        
        CurrentSkill = _skills[_currentSkillIndex];

        _meshRenderer.material.color = GetColor(CurrentSkill);
    }

    public bool TryUseCurrentSkill()
    {
        switch (CurrentSkill)
        {
            case Hacking:
                return TryUseSkill<IHackable, Hacking>();
            case Shooting:
                _shootParticles.Play();
                return TryUseSkill<IShootable, Shooting>();
            default:
                return false;
        }
    }
    
    // Unity handles the event {shift held down} in OnGUI
    // But ActionInputHandler is not Monobehaviour...
    // So I'm gonna handle shift events here...
    public void TryUseAcceleration()
    {
        if (_owner is IAcceleratable acceleratableOwner)
        {
            if (CurrentSkill is Acceleration acceleration)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    acceleratableOwner.TryAccelerate(acceleration.Value);
                }
                else
                {
                    acceleratableOwner.ResetAcceleration();
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    acceleratableOwner.ResetAcceleration();
                }
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
        var target = _owner.TryGetTarget<T0>();
        var skill = TryGetSkill<T1>();
        
        if (skill != null)
        {
            return skill.TryActivate(target);
        }

        return false;
    }

    private T TryGetSkill<T>() where T : Skill
    {
        return _skills.FirstOrDefault(s => s is T) as T;
    }

    private Color GetColor(Skill skill)
    {
        return skill switch
        {
            Shooting => _skillColors[0],
            Hacking => _skillColors[1],
            Acceleration => _skillColors[2],
            _ => _defaultColor
        };
    }

    public bool Usable()
    {
        return _skills.Count != 0;
    }
}