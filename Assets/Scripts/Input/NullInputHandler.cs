public class NullInputHandler : InputHandler
{
    public NullInputHandler(PauseMenu pauseMenu) : base(pauseMenu){}

    public override void Handle() {}

    public override void SetCursor(CursorSwitcher cursorSwitcher)
    {
        cursorSwitcher.SwitchToCursor();
    }
}