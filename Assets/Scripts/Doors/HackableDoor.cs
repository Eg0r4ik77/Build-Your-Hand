using System;
using GameState;
using PuzzleGames;
using Skills;
using UnityEngine;

namespace Doors
{
    public class HackableDoor : Door, IHackable
    {
        [SerializeField] private PuzzleGame _puzzleGame;
        public event Action GameLeft;
        public event Action<PuzzleGame> HackingStarted; 

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
                HackingStarted?.Invoke(_puzzleGame);
                _puzzleGame.gameObject.SetActive(true);
                _puzzleGame.StartGame();
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