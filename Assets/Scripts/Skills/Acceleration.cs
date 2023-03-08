namespace Skills
{
    public class Acceleration : Skill
    {
        public float Value { get; }

        public Acceleration(float value)
        {
            Value = value;
        }
        
        public override bool TryActivate(ISkillTarget target)
        {
            if (target is IAcceleratable acceleratableTarget)
            {
                acceleratableTarget.TryAccelerate(Value);
            }
            
            return true;
        }

        public void Deactivate(ISkillTarget target)
        {
            if (target is IAcceleratable acceleratableTarget)
            {
                acceleratableTarget.ResetAcceleration();
            }
        }
    }
}