using PlayerInput;
using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public class StandState : GameState
    {
        public StandState(Image predictionPoint) : base(predictionPoint){}

        public override void Start()
        {
            PredictionPointImage.gameObject.SetActive(false);
            SetCursor();
        }

        public override void SetCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}