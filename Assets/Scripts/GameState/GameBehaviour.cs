using System.Collections.Generic;
using System.Linq;
using PlayerInput;
using UnityEngine;

namespace GameState
{
    public class GameBehaviour : MonoBehaviour, IGameStateSwitcher
    {
        private InputManager _inputManager;
        private CursorSwitcher _cursorSwitcher;
        
        private List<GameState> _gameStates;

        private void Awake()
        {
            _inputManager = FindObjectOfType<InputManager>();
            _cursorSwitcher = FindObjectOfType<CursorSwitcher>();
        }

        private void Start()
        {
            _gameStates = new List<GameState>()
            {
                new ActionState(_cursorSwitcher),
                new StandState(_cursorSwitcher)
            };
        
            SwitchState<ActionState>();
        }

        private void OnEnable()
        {
            _inputManager.SwitchedToAction += SwitchState<ActionState>;
            _inputManager.SwitchedToStand += SwitchState<StandState>;
        }

        private void OnDisable()
        {
            _inputManager.SwitchedToAction -= SwitchState<ActionState>;
            _inputManager.SwitchedToStand -= SwitchState<StandState>;
        }

        public void SwitchState<T>() where T : GameState
        {
            GameState state = _gameStates.FirstOrDefault(state => state is T);
            state?.Start();
        }
    }
}