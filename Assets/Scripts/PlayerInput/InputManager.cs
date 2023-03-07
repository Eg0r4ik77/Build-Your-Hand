using System.Collections.Generic;
using System.Linq;
using Doors;
using Economy;
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
        [SerializeField] private Shop _shop;
        
        private PuzzleInputHandler _puzzleInputHandler;
        private ShopInputHandler _shopInputHandler;
        
        private List<InputHandler> _inputHandlers;

        private InputHandler _currentInputHandler;
        
        private List<HackableDoor> _hackableDoors;

        private void Awake()
        {
            _puzzleInputHandler = new PuzzleInputHandler();
            _shopInputHandler = new ShopInputHandler();
            
            // з о ч е м
            _inputHandlers = new List<InputHandler> { _actionInputHandler, _puzzleInputHandler, _shopInputHandler };
            
            _hackableDoors = FindObjectsOfType<HackableDoor>().ToList();
        }

        private void Start()
        {
            SwitchInputHandling<ActionInputHandler>();
        }

        private void OnEnable()
        {
            _shop.Opened += SwitchToShop;
            _shopInputHandler.SwitchedToAction += _gameBehaviour.SwitchState<ActionState>;
            
            foreach (HackableDoor hackableDoor in _hackableDoors)
            {
                hackableDoor.Hacked += SwitchToPuzzle;
            }
            
            _puzzleInputHandler.SwitchedToAction += _gameBehaviour.SwitchState<ActionState>;
        }

        private void OnDisable()
        {
            _shop.Opened -= SwitchToShop;
            _shopInputHandler.SwitchedToAction -= _gameBehaviour.SwitchState<ActionState>;
            
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

        private void SwitchToShop(Shop shop)
        {
            _shopInputHandler.SetShop(shop);
            _gameBehaviour.SwitchState<PuzzleState>();
            SwitchInputHandling<ShopInputHandler>();
        }
    }
}