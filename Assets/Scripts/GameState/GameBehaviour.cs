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
        
        private GameState _currentGameState;
        private List<GameState> _gameStates;

        private void Start()
        {
            _gameStates = new List<GameState>()
            {
                new ActionState(_inputManager, _predictionPointImage,this),
                new PuzzleState(_inputManager, _predictionPointImage, this)
            };
        
            SwitchState<ActionState>();
        }

        public void SwitchState<T>() where T : GameState
        {
            GameState state = _gameStates.FirstOrDefault(state => state is T);

            _currentGameState?.Stop();
       
            state.Start();
            _currentGameState = state;
        }
    }
}