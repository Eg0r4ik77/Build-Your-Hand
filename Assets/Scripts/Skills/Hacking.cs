namespace Skills
{
    public class Hacking : Skill
    {
        public override bool TryActivate(ISkillTarget target)
        {
            if (target is IHackable hackableTarget)
            {
                return hackableTarget.TryHack();
            }

            return true;
        }
    }
}