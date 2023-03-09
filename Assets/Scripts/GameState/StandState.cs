namespace GameState
{
    public class StandState : GameState
    {
        public StandState(CursorSwitcher switcher) : base(switcher) {}
        
        public override void Start()
        {
            Switcher.SwitchPredictionPointToCursor();
        }
    }
}