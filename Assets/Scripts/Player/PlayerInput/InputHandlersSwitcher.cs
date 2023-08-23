using System.Collections.Generic;
using System.Linq;
using Economy;
using PlayerInput;
using PuzzleGames;
using UnityEngine;

public class InputHandlersSwitcher : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private PuzzleGamesSwitcher _puzzleGamesSwitcher;
    [SerializeField] private Shop _shop;
    [SerializeField] private CursorSwitcher _cursorSwitcher;

    private List<InputHandler> _inputHandlers;
    private InputHandler _currentInputHandler;

    private void Awake()
    {
        _inputHandlers = new List<InputHandler>
        { 
            new ActionInputHandler(_player, _shop, _pauseMenu),
            //new pause handler
            new ShopInputHandler(_shop)
        };
    }

    private void Start()
    {
        SwitchToAction();
    }

    private void OnEnable()
    {
        _pauseMenu.Opened += SwitchToPauseMenu;
        //переключение должно быть к предыдущему хендлеру, а не к action
        _pauseMenu.Closed += SwitchToAction;
        
        _shop.Opened += SwitchToShop;
        _shop.Closed += SwitchToAction;

        _puzzleGamesSwitcher.Opened += SwitchToPuzzle;
        _puzzleGamesSwitcher.Closed += SwitchToAction;
    }

    private void OnDisable()
    {
        _pauseMenu.Opened -= SwitchToPauseMenu;
        //переключение должно быть к предыдущему хендлеру, а не к action
        _pauseMenu.Closed -= SwitchToAction;

        _shop.Opened -= SwitchToShop;
        _shop.Closed -= SwitchToAction;

        _puzzleGamesSwitcher.Opened -= SwitchToPuzzle;
        _puzzleGamesSwitcher.Closed -= SwitchToAction;
    }

    private void Update()
    {
        _currentInputHandler.Handle();
    }

    private void SwitchToPuzzle(PuzzleGame game)
    {
        _currentInputHandler = new PuzzleInputHandler(game);
        
        _cursorSwitcher.SwitchPredictionPointToCursor();
        SwitchInputHandling<PuzzleInputHandler>();
    }

    private void SwitchToShop()
    {
        _cursorSwitcher.SwitchPredictionPointToCursor();
        SwitchInputHandling<ShopInputHandler>();
    }

    private void SwitchToPauseMenu()
    {
        _cursorSwitcher.SwitchPredictionPointToCursor();
    }

    private void SwitchToAction()
    {
        _cursorSwitcher.SwitchCursorToPredictionPoint();
        SwitchInputHandling<ActionInputHandler>();
    }
    
    private void SwitchInputHandling<T>() where T : InputHandler
    {
        InputHandler handler = _inputHandlers.FirstOrDefault(handler => handler is T);
        
        if (handler != null)
        {
            _currentInputHandler = handler;
        }
    }
}