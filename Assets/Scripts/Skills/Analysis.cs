using UnityEngine;

namespace Skills
{
    public class Analysis : Skill
    {
        public override bool TryActivate(ISkillTarget target)
        {
            Debug.Log("Analysis");
            return true;
        }
    }
}