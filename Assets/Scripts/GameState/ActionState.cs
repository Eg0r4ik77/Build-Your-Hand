using PlayerInput;
using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public class ActionState : GameState
    {
        public ActionState(InputManager input, Image predictionPoint,IGameStateSwitcher gameStateSwitcher)
            : base(input, predictionPoint, gameStateSwitcher){}

        public override void Start()
        {
            PredictionPointImage.gameObject.SetActive(true);
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