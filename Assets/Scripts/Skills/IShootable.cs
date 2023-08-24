namespace Skills
{
    public interface IShootable : ISkillTarget
    {
        void TryApplyShoot(Player player, float damage);
    }
}