using System;
using UnityEngine;

namespace PlayerInput
{
    public class PuzzleInputHandler : InputHandler
    {
        public event Action SwitchedToAction;
          

        public override void Handle()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwitchedToAction?.Invoke();
            }
        }
    }
}