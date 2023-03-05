using GameState;
using PuzzleGames;
using UnityEngine;

namespace Doors
{
    public class HackableDoor : Door, IHackable
    {
        [SerializeField] private GameBehaviour _gameBehaviour;
        [SerializeField] private PuzzleGame _puzzleGame;

        private bool _hacked;
        
        private void OnEnable()
        {
            _puzzleGame.Finished += _gameBehaviour.SwitchState<ActionState>;
            _puzzleGame.Finished += SetHacked;
        }

        private void OnDisable()
        {
            _puzzleGame.Finished -= _gameBehaviour.SwitchState<ActionState>;
            _puzzleGame.Finished -= SetHacked;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_hacked && other.TryGetComponent(out Hacking hacking))
            {
                hacking.SetHackable(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_hacked && other.TryGetComponent(out Hacking hacking))
            {
                hacking.SetHackable(null);
            }
        }

        public void ApplyHack()
        {
            _puzzleGame.gameObject.SetActive(true);
            _puzzleGame.StartGame();
        }

        private void SetHacked()
        {
            _hacked = true;
        }
    }
}