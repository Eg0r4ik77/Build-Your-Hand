using System.Collections.Generic;
using System.Linq;
using GameState;
using UnityEngine;

namespace PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameBehaviour _gameBehaviour;
        
        [SerializeField] private ActionInputHandler _actionInputHandler;
        [SerializeField] private PlayerMovement _playerMovement;
    
        private PuzzleInputHandler _puzzleInputHandler;
        private List<InputHandler> _inputHandlers;
        
        private InputHandler _currentInputHandler;

        private void Awake()
        {
            _puzzleInputHandler = new PuzzleInputHandler();
            _inputHandlers = new List<InputHandler> { _actionInputHandler, _puzzleInputHandler };
        }

        private void Start()
        {

            SwitchInputHandling<ActionInputHandler>();
        }

        private void OnEnable()
        {
            _actionInputHandler.SwitchedToPuzzle += _gameBehaviour.SwitchState<PuzzleState>;
            _puzzleInputHandler.SwitchedToAction += _gameBehaviour.SwitchState<ActionState>;
        }

        private void OnDisable()
        {
            _actionInputHandler.SwitchedToPuzzle -= _gameBehaviour.SwitchState<PuzzleState>;
            _puzzleInputHandler.SwitchedToAction -= _gameBehaviour.SwitchState<ActionState>;
        }

        private void Update()
        {
            _currentInputHandler.Handle();
        }

        public void SwitchInputHandling<T>() where T : InputHandler
        {
            InputHandler handler = _inputHandlers.FirstOrDefault(handler => handler is T);
            handler.SetPlayer(_playerMovement);
            _currentInputHandler = handler;
        }
    }
}