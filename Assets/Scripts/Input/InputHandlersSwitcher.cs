using System.Collections.Generic;
using System.Linq;
using Economy;
using PuzzleGames;
using UnityEngine;
using Zenject;

public class InputHandlersSwitcher : MonoBehaviour
{
    private Player _player;
    private CursorSwitcher _cursorSwitcher;
    private PauseMenu _pauseMenu; 
    private PuzzleGamesSwitcher _puzzleGamesSwitcher;
    private Shop _shop;
    private GameFinisher _gameFinisher;
    
    private List<InputHandler> _inputHandlers;
    
    private InputHandler _previousInputHandler;
    private InputHandler _currentInputHandler;

    [Inject]
    private void Construct(Player player, CursorSwitcher cursorSwitcher, PauseMenu pauseMenu,
        PuzzleGamesSwitcher puzzleGamesSwitcher, Shop shop, GameFinisher gameFinisher)
    {
        _player = player;
        _cursorSwitcher = cursorSwitcher;
        _pauseMenu = pauseMenu;
        _puzzleGamesSwitcher = puzzleGamesSwitcher;
        _shop = shop;
        _gameFinisher = gameFinisher;
    }
    
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

        _gameFinisher.GameFinished += SwitchToNull;
    }

    private void OnDisable()
    {
        _pauseMenu.Opened -= SwitchToPauseMenu;
        _pauseMenu.Closed -= SwitchToPreviousInputHandler;

        _shop.Opened -= SwitchToShop;
        _shop.Closed -= SwitchToAction;

        _puzzleGamesSwitcher.Opened -= SwitchToPuzzle;
        _puzzleGamesSwitcher.Closed -= SwitchToAction;
        
        _gameFinisher.GameFinished -= SwitchToNull;
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
    

    private void SwitchToNull()
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