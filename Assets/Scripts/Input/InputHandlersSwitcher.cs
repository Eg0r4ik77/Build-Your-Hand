using System.Collections.Generic;
using System.Linq;
using Economy;
using PuzzleGames;
using UnityEngine;

public class InputHandlersSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private PuzzleGamesSwitcher _puzzleGamesSwitcher;
    [SerializeField] private Shop _shop;
    [SerializeField] private CursorSwitcher _cursorSwitcher;
    [SerializeField] private GameFinisher _gameFinisher;

    private List<InputHandler> _inputHandlers;
    
    private InputHandler _previousInputHandler;
    private InputHandler _currentInputHandler;
    
    private void Start()
    {
        _inputHandlers = new List<InputHandler>
        { 
            new ActionInputHandler(_player, _shop, _pauseMenu),
            new PauseMenuInputHandler(_pauseMenu),
            new ShopInputHandler(_shop, _pauseMenu),
            new NullInputHandler(_pauseMenu)
        };
        
        SwitchToAction();
    }

    private void OnEnable()
    {
        _pauseMenu.Opened += SwitchToPauseMenu;
        _pauseMenu.Closed += SwitchToPreviousInputHandler;
        
        _shop.Opened += SwitchToShop;
        _shop.Closed += SwitchToAction;

        _puzzleGamesSwitcher.Opened += SwitchToPuzzle;
        _puzzleGamesSwitcher.Closed += SwitchToAction;

        _gameFinisher.GameFinished += DisableHandling;
    }

    private void OnDisable()
    {
        _pauseMenu.Opened -= SwitchToPauseMenu;
        _pauseMenu.Closed -= SwitchToPreviousInputHandler;

        _shop.Opened -= SwitchToShop;
        _shop.Closed -= SwitchToAction;

        _puzzleGamesSwitcher.Opened -= SwitchToPuzzle;
        _puzzleGamesSwitcher.Closed -= SwitchToAction;
        
        _gameFinisher.GameFinished -= DisableHandling;
    }

    private void Update()
    {
        _currentInputHandler.Handle();
    }
    
    private void SwitchToAction()
    {
        SwitchInputHandling<ActionInputHandler>();
    }

    private void SwitchToPuzzle(PuzzleGame game)
    {
        var puzzleInputHandler = new PuzzleInputHandler(game, _pauseMenu);
        SetCurrentInputHandler(puzzleInputHandler);
    }

    private void SwitchToShop()
    {
        SwitchInputHandling<ShopInputHandler>();
    }

    private void SwitchToPauseMenu()
    {
        SwitchInputHandling<PauseMenuInputHandler>();
    }
    

    private void DisableHandling()
    {
        SwitchInputHandling<NullInputHandler>();
    }
    
    private void SwitchInputHandling<T>() where T : InputHandler
    {
        InputHandler handler = _inputHandlers.FirstOrDefault(handler => handler is T);
        
        if (handler != null)
        {
            SetCurrentInputHandler(handler);
        }
    }
    
    private void SwitchToPreviousInputHandler()
    {
        SetCurrentInputHandler(_previousInputHandler);
    }
    
    private void SetCurrentInputHandler(InputHandler handler)
    {
        _previousInputHandler = _currentInputHandler;
        _currentInputHandler = handler;
        handler.SetCursor(_cursorSwitcher);
    }
}