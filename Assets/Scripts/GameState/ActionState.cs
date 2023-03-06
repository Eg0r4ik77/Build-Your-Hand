using PlayerInput;
using UnityEngine;

namespace GameState
{
    public class ActionState : GameState
    {
        public ActionState(InputManager input, IGameStateSwitcher gameStateSwitcher) : base(input, gameStateSwitcher) {}

        public override void Start()
        {
            SetCursor();
            SetInputHandling();
        }

        public override void Stop() {}

        public override void SetInputHandling()
        {
            Input.SwitchInputHandling<ActionInputHandler>();
        }

        public override void SetCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}