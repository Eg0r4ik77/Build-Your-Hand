﻿using System;
using System.Collections.Generic;
using System.Linq;
using Skills;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UniversalHand : MonoBehaviour
{
    [SerializeField] private List<Color> _skillColors;
    [SerializeField] private ParticleSystem _shootParticles;
    
    private Color _defaultColor;

    private Player _player;
    
    private List<Skill> _skills;
    private int _currentSkillIndex;

    private MeshRenderer _meshRenderer;
    
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

    public void SetPlayer(Player player)
    {
        _player = player;
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
    
    public void AddSkill(Skill skill)
    {
        _skills.Add(skill);
    }

    public bool TryUseSkill<T0, T1>() 
        where T0 : ISkillTarget
        where T1 : Skill
    {
        var target = _player.TryGetTarget<T0>();
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
            Analysis => _skillColors[2],
            _ => _defaultColor
        };
    }

    public bool Usable()
    {
        return _skills.Count != 0;
    }
}