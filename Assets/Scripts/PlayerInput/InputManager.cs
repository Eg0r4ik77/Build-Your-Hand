using System.Collections.Generic;
using System.Linq;
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
            _actionInputHandler.SwitchedToPuzzle += OnSwitchedToPuzzle;
            _puzzleInputHandler.SwitchedToAction += _gameBehaviour.SwitchState<ActionState>;
        }

        private void OnDisable()
        {
            _actionInputHandler.SwitchedToPuzzle -= OnSwitchedToPuzzle;
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

        private void OnSwitchedToPuzzle(PuzzleGame game)
        {
            _puzzleInputHandler.SetPuzzleGame(game);
            _gameBehaviour.SwitchState<PuzzleState>();
        }
    }
}