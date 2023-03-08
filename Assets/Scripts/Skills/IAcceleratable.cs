namespace Skills
{
    public interface IAcceleratable : ISkillTarget
    {
        void TryAccelerate(float acceleration);
        void ResetAcceleration();
    }
}