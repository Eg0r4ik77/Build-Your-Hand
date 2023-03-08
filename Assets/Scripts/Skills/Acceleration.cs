using UnityEngine;

namespace Skills
{
    public class Acceleration : Skill
    {
        public override bool TryActivate(ISkillTarget target)
        {
            if (target is IAcceleratable acceleratableTarget)
            {
                return acceleratableTarget.TryAccelerate();
            }
            
            return true;
        }
    }
}