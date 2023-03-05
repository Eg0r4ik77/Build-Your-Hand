using System;
using UnityEngine;

namespace PlayerInput
{
    [Serializable]
    public class ActionInputHandler : InputHandler
    {
        [SerializeField] private float _walkSpeed = 3f;

        [SerializeField, Range(1, 10)] private float _lookSpeedX = 2f;
        [SerializeField, Range(1, 10)] private float _lookSpeedY = 2f;
    
        [SerializeField, Range(1, 180)] private float _upperLookLimit = 80f;
        [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80f;

        [SerializeField] private Camera _camera;

        private float _rotationX;

        private float VerticalAxis => Input.GetAxis("Vertical");
        private float HorizontalAxis => Input.GetAxis("Horizontal");
    
        private float MouseHorizontalAxis => Input.GetAxis("Mouse X");
        private float MouseVerticalAxis => Input.GetAxis("Mouse Y");

        public event Action SwitchedToPuzzle;

        public override void Handle()
        {
            HandleMovementInput();
            HandleMouseLook();


            var hacking = Player.GetComponent<Hacking>();
            if (Input.GetKeyDown(KeyCode.E) && hacking.CanHack)
            {
                hacking.Hack();
                SwitchedToPuzzle?.Invoke();
            }
        }
    
        private void HandleMovementInput()
        {
            Vector3 motion = Player.transform.forward * VerticalAxis + Player.transform.right * HorizontalAxis;
            Player.Move(motion * (_walkSpeed * Time.deltaTime));
        }

        private void HandleMouseLook()
        {
            _rotationX -= MouseVerticalAxis * _lookSpeedY;
            _rotationX = Mathf.Clamp(_rotationX, -_upperLookLimit, _lowerLookLimit);
        
            _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            Player.transform.rotation *= Quaternion.Euler(0, MouseHorizontalAxis * _lookSpeedX, 0);
        }
    }
}