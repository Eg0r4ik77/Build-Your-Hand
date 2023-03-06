using System;
using GameState;
using PuzzleGames;
using Skills;
using UnityEngine;

namespace Doors
{
    public class HackableDoor : Door, IHackable
    {
        [SerializeField] private GameBehaviour _gameBehaviour;
        [SerializeField] private PuzzleGame _puzzleGame;

        public event Action<PuzzleGame> Hacked; 

        private void OnEnable()
        {
            _puzzleGame.Finished += _gameBehaviour.SwitchState<ActionState>;
            _puzzleGame.Finished += OnHacked;
        }

        private void OnDisable()
        {
            _puzzleGame.Finished -= _gameBehaviour.SwitchState<ActionState>;
            _puzzleGame.Finished -= OnHacked;
        }

        private void OnHacked()
        {
            Open();
        }

        public bool TryHack()
        {
            if (!_puzzleGame.IsFinished)
            {
                Hacked?.Invoke(_puzzleGame);
                _puzzleGame.gameObject.SetActive(true);
                _puzzleGame.StartGame();
                return true;
            }

            return false;
        }
    }
}