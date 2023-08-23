using UnityEngine;

public class PauseMenuInputHandler : InputHandler
{
    private readonly PauseMenu _pauseMenu;

    private bool IsClosePauseMenuInput => Input.GetKeyDown(KeyCode.Escape);
    
    public PauseMenuInputHandler(PauseMenu pauseMenu) : base(pauseMenu)
    {
        _pauseMenu = pauseMenu;
    }

    public override void Handle()
    {
        if (IsClosePauseMenuInput)
        {
            _pauseMenu.ClosePausePanel();
        }
    }

    public override void SetCursor(CursorSwitcher cursorSwitcher)
    {
        cursorSwitcher.SwitchToCursor();
    }
}
