namespace Skills
{
    public interface IHackable : ISkillTarget
    {
        bool TryHack();
    }
}