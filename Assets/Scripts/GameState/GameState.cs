using PlayerInput;
using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public abstract class GameState
     {
         protected readonly InputManager Input;
         protected Image PredictionPointImage; 
         
         protected readonly IGameStateSwitcher GameStateSwitcher;
     
         protected GameState(InputManager input, Image predictionPoint, IGameStateSwitcher gameStateSwitcher)
         {
             Input = input;
             PredictionPointImage = predictionPoint;
             GameStateSwitcher = gameStateSwitcher;
         }
     
         public abstract void Start();
         public abstract void Stop();
         public abstract void SetInputHandling();
         public abstract void SetCursor();
     }
}
