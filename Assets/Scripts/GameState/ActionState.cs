using UnityEngine;
using UnityEngine.UI;

namespace GameState
{
    public class ActionState : GameState
    {
        public ActionState(Image predictionPoint) : base(predictionPoint){}

        public override void Start()
        {
            PredictionPointImage.gameObject.SetActive(true);
            SetCursor();
        }

        public override void SetCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}