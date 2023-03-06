using System.Collections.Generic;
using System.Linq;
using Doors;
using GameState;
using PuzzleGames;
using UnityEngine;

namespace PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameBehaviour _gameBehaviour;
        
        [SerializeField] private ActionInputHandler _actionInputHandler;
        [SerializeField] private Player _player;
    
        private PuzzleInputHandler _puzzleInputHandler;
        private List<InputHandler> _inputHandlers;
        
        private InputHandler _currentInputHandler;
        private List<HackableDoor> _hackableDoors;

        private void Awake()
        {
            _puzzleInputHandler = new PuzzleInputHandler();
            _inputHandlers = new List<InputHandler> { _actionInputHandler, _puzzleInputHandler };
            _hackableDoors = FindObjectsOfType<HackableDoor>().ToList();
        }

        private void Start()
        {
            SwitchInputHandling<ActionInputHandler>();
        }

        private void OnEnable()
        {
            foreach (HackableDoor hackableDoor in _hackableDoors)
            {
                hackableDoor.Hacked += SwitchToPuzzle;
            }
            
            _puzzleInputHandler.SwitchedToAction += _gameBehaviour.SwitchState<ActionState>;
        }

        private void OnDisable()
        {
            foreach (HackableDoor hackableDoor in _hackableDoors)
            {
                hackableDoor.Hacked -= SwitchToPuzzle;
            }
            
            _puzzleInputHandler.SwitchedToAction -= _gameBehaviour.SwitchState<ActionState>;
        }

        private void Update()
        {
            _currentInputHandler.Handle();
        }

        public void SwitchInputHandling<T>() where T : InputHandler
        {
            InputHandler handler = _inputHandlers.FirstOrDefault(handler => handler is T);
            handler.SetPlayer(_player);
            _currentInputHandler = handler;
        }

        private void SwitchToPuzzle(PuzzleGame game)
        {
            _puzzleInputHandler.SetPuzzleGame(game);
            _gameBehaviour.SwitchState<PuzzleState>();
        }
    }
}