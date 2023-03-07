namespace Skills
{
    public class Shooting : Skill
    {
        private float _damage;

        public Shooting(float damage)
        {
            _damage = damage;
        }
        
        public override bool TryActivate(ISkillTarget target)
        {
            if (target is IShootable shootableTarget)
            {
                shootableTarget.TryApplyShoot(_damage);
            }

            return true;
        }
    }
}