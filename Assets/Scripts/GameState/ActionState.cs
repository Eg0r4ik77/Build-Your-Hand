namespace GameState
{
    public class ActionState : GameState
    {
        public ActionState(CursorSwitcher switcher) : base(switcher) {}

        public override void Start()
        {
            Switcher.SwitchCursorToPredictionPoint();
        }
    }
}