namespace Skills
{
    public interface ISkillTarget
    {
        bool TryApplySkill(Skill skill);
    }
}