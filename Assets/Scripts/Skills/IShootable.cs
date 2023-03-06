namespace Skills
{
    public interface IShootable : ISkillTarget
    {
        void TryApplyShoot(float damage);
    }
}