using PlayerInput;

namespace GameState
{
    public abstract class GameState
     {
         protected readonly InputManager Input;
         protected readonly IGameStateSwitcher GameStateSwitcher;
     
         protected GameState(InputManager input, IGameStateSwitcher gameStateSwitcher)
         {
             Input = input;
             GameStateSwitcher = gameStateSwitcher;
         }
     
         public abstract void Start();
         public abstract void Stop();
         public abstract void SetInputHandling();
         public abstract void SetCursor();
     }
}
