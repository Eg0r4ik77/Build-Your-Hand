namespace GameState
{
    public interface IGameStateSwitcher
    {
        void SwitchState<T>() where T : GameState;
    }
}