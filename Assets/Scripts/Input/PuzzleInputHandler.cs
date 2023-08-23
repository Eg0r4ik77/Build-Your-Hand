using PuzzleGames;
using UnityEngine;

public class PuzzleInputHandler : InputHandler
{
    private readonly PuzzleGame _currentGame;
    
    private bool IsCloseGameInput => Input.GetKeyDown(KeyCode.E);

    public PuzzleInputHandler(PuzzleGame game, PauseMenu pauseMenu) : base(pauseMenu)
    {
        _currentGame = game;
    }
    
    public override void Handle()
    {
        base.Handle();
        
        if (IsCloseGameInput)
        {
            _currentGame.InterruptGame();
        }
    }

    public override void SetCursor(CursorSwitcher cursorSwitcher)
    {
        cursorSwitcher.SwitchToCursor();
    }
}