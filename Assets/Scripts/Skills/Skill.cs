using Economy;

namespace Skills
{
    public abstract class Skill: IPurchasable
    {
        public float Cost { get; set; }
        public abstract bool TryActivate(ISkillTarget target);
    }
}