using GameState;
using PuzzleGames;
using UnityEngine;

namespace Doors
{
    public class HackableDoor : Door, IHackable
    {
        [SerializeField] private GameBehaviour _gameBehaviour;
        [SerializeField] private PuzzleGame _puzzleGame;

        public PuzzleGame Game => _puzzleGame;
        
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

        public bool TryApplySkill(Skill skill)
        {
            if (skill == Skill.Hacking)
            {
                return TryHack();
            }

            return false;
        }

        public bool TryHack()
        {
            if (!_puzzleGame.IsFinished)
            {
                _puzzleGame.gameObject.SetActive(true);
                _puzzleGame.StartGame();
                return true;
            }

            return false;
        }
    }
}