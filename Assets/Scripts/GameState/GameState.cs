using UnityEngine.UI;

namespace GameState
{
    public abstract class GameState
     {
         protected readonly CursorSwitcher Switcher;

         protected GameState(CursorSwitcher switcher)
         {
             Switcher = switcher;
         }
     
         public abstract void Start();
     }
}
