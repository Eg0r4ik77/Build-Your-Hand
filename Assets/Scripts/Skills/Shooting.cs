namespace Skills
{
    public class Shooting : Skill
    {
        private readonly Player _player;
        private readonly float _damage;

        public Shooting(Player player, float damage)
        {
            _player = player;
            _damage = damage;
        }
        
        public override bool TryActivate(ISkillTarget target)
        {
            if (target is IShootable shootableTarget)
            {
                shootableTarget.TryApplyShoot(_player, _damage);
            }

            return true;
        }
    }
}