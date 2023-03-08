using System;
using PuzzleGames;
using Skills;
using UnityEngine;

namespace Doors
{
    public class HackableDoor : Door, IHackable
    {
        [SerializeField] private PuzzleGame _puzzleGame;

        public event Action<PuzzleGame> GameStarted; 
        public event Action GameLeft;

        private void OnEnable()
        {
            _puzzleGame.Finished += ApplyHack;
            _puzzleGame.Interrupted += LeaveGame;
        }

        private void OnDisable()
        {
            _puzzleGame.Finished -= ApplyHack;
            _puzzleGame.Interrupted -= LeaveGame;
        }
        
        public bool TryHack()
        {
            if (!_puzzleGame.IsFinished)
            {
                GameStarted?.Invoke(_puzzleGame);
                return true;
            }

            return false;
        }

        private void ApplyHack()
        {
            LeaveGame();
            Open();
        }

        private void LeaveGame()
        {
            GameLeft?.Invoke();
        }
    }
}