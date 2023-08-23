using UnityEngine;

public abstract class InputHandler
{
    private readonly PauseMenu _pauseMenu;

    private bool IsOpenPauseMenuInput => Input.GetKeyDown(KeyCode.Escape);

    protected InputHandler(PauseMenu pauseMenu)
    {
        _pauseMenu = pauseMenu;
    }
    
    public virtual void Handle()
    {
        if (IsOpenPauseMenuInput)
        {
            _pauseMenu.OpenPausePanel();
        }
    }
    public abstract void SetCursor(CursorSwitcher cursorSwitcher);
}