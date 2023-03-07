namespace Skills
{
    public abstract class Skill
    {
        public abstract bool TryActivate(ISkillTarget target);
    }
}