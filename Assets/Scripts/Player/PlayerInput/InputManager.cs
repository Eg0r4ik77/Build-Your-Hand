using System;
using System.Collections.Generic;
using System.Linq;
using Doors;
using Economy;
using PuzzleGames;
using UnityEngine;

namespace PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private ActionInputHandler _actionInputHandler;
        [SerializeField] private Shop _shop;
        
        private PuzzleInputHandler _puzzleInputHandler;
        private ShopInputHandler _shopInputHandler;
        private InputHandler _currentInputHandler;
        
        private List<InputHandler> _inputHandlers;

        private List<HackableDoor> _hackableDoors;
        
        public event Action SwitchedToAction; 
        public event Action SwitchedToStand; 

        private void Awake()
        {
            _puzzleInputHandler = new PuzzleInputHandler();
            _shopInputHandler = new ShopInputHandler();
            
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
            _shopInputHandler.SwitchedToAction += SwitchToAction;
            
            foreach (HackableDoor hackableDoor in _hackableDoors)
            {
                hackableDoor.GameStarted += SwitchToPuzzle;
                hackableDoor.GameLeft += SwitchToAction;
            }
            _puzzleInputHandler.SwitchedToAction += SwitchToAction;
        }

        private void OnDisable()
        {
            _shop.Opened -= SwitchToShop;
            _shopInputHandler.SwitchedToAction -= SwitchToAction;
            
            foreach (HackableDoor hackableDoor in _hackableDoors)
            {
                hackableDoor.GameStarted -= SwitchToPuzzle;
            }
            _puzzleInputHandler.SwitchedToAction -= SwitchToAction;
        }

        private void Update()
        {
            _currentInputHandler.Handle();
        }

        public void SwitchInputHandling<T>() where T : InputHandler
        {
            InputHandler handler = _inputHandlers.FirstOrDefault(handler => handler is T);
            
            if (handler != null)
            {
                handler.SetPlayer(_player);
                _currentInputHandler = handler;
            }
        }

        private void SwitchToPuzzle(PuzzleGame game)
        {
            _puzzleInputHandler.SetPuzzleGame(game);
            
            SwitchedToStand?.Invoke();
            SwitchInputHandling<PuzzleInputHandler>();
        }

        private void SwitchToShop(Shop shop)
        {
            _shopInputHandler.SetShop(shop);
            
            SwitchedToStand?.Invoke();
            SwitchInputHandling<ShopInputHandler>();
        }

        private void SwitchToAction()
        {
            SwitchedToAction?.Invoke();
            SwitchInputHandling<ActionInputHandler>();
        }
    }
}