using System.Collections.Generic;
using System.Linq;
using PlayerInput;
using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public class GameBehaviour : MonoBehaviour, IGameStateSwitcher
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Image _predictionPointImage;
        
        private List<GameState> _gameStates;

        private void Start()
        {
            _gameStates = new List<GameState>()
            {
                new ActionState(_predictionPointImage),
                new StandState(_predictionPointImage)
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
            state.Start();
        }
    }
}