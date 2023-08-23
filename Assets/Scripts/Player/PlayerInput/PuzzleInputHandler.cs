using PuzzleGames;
using UnityEngine;

public class PuzzleInputHandler : InputHandler
{
    private readonly PuzzleGame _currentGame;
    
    private bool IsCloseGameInput => Input.GetKeyDown(KeyCode.E);

    public PuzzleInputHandler(PuzzleGame game)
    {
        _currentGame = game;
    }
    
    public void Handle()
    {
        if (IsCloseGameInput)
        {
            _currentGame.InterruptGame();
        }
    }
}