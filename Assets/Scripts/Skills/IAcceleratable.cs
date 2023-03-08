namespace Skills
{
    public interface IAcceleratable : ISkillTarget
    {
        bool TryAccelerate();
    }
}