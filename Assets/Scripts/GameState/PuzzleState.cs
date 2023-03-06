using PlayerInput;
using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public class PuzzleState : GameState
    {
        public PuzzleState(InputManager input, Image predictionPoint,IGameStateSwitcher gameStateSwitcher)
            : base(input, predictionPoint, gameStateSwitcher){}

        public override void Start()
        {
            PredictionPointImage.gameObject.SetActive(false);
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