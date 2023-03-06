using System;
using PuzzleGames;
using UnityEngine;

namespace PlayerInput
{
    public class PuzzleInputHandler : InputHandler
    {
        private PuzzleGame _currentGame;
        public event Action SwitchedToAction;

        public void SetPuzzleGame(PuzzleGame game)
        {
            _currentGame = game;
        }

        public override void Handle()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _currentGame.InterruptGame();
                SwitchedToAction?.Invoke();
            }
        }
    }
}