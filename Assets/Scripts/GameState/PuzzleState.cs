using PlayerInput;
using UnityEngine;

namespace GameState
{
    public class PuzzleState : GameState
    {
        public PuzzleState(InputManager input, IGameStateSwitcher gameStateSwitcher) : base(input, gameStateSwitcher) {}

        public override void Start()
        {
            SetCursor();
            SetInputHandling();
        }

        public override void Stop() {}

        public override void SetInputHandling()
        {
            Input.SwitchInputHandling<PuzzleInputHandler>();
        }

        public override void SetCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}