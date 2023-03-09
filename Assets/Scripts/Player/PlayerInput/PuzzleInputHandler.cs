using System;
using PuzzleGames;
using UnityEngine;

namespace PlayerInput
{
    public class PuzzleInputHandler : InputHandler
    {
        private PuzzleGame _currentGame;
        
        private bool IsCloseGameInput => Input.GetKeyDown(KeyCode.E);
        
        public event Action SwitchedToAction;

        public void SetPuzzleGame(PuzzleGame game)
        {
            _currentGame = game;
        }

        public override void Handle()
        {
            if (IsCloseGameInput)
            {
                _currentGame.InterruptGame();
                SwitchedToAction?.Invoke();
            }
        }
    }
}