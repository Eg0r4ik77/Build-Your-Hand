namespace Enemies.BattleSequence
{
    public interface IBattleSequence
    {
        void InitializeSequence();
        void UpdateSequenceScenario();
        void FinishSequence();
    }
}