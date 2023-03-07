using System.Collections.Generic;
using System.Linq;
using Skills;

public class UniversalHand
{
    private Player _player;
    private List<Skill> _skills;

    public UniversalHand(Player player)
    {
        _player = player;
        _skills = new List<Skill>();
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
        
        if (target != null && skill != null)
        {
            return skill.TryActivate(target);
        }

        return false;
    }

    private T TryGetSkill<T>() where T : Skill
    {
        return _skills.FirstOrDefault(s => s is T) as T;
    }
}