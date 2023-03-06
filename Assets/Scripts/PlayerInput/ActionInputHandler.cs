using System;
using Skills;
using UnityEngine;

namespace PlayerInput
{
    [Serializable]
    public class ActionInputHandler : InputHandler
    {
        [SerializeField, Range(1, 10)] private float _horizontalLookSpeed = 2f;
        
        [SerializeField] private FirstPersonCamera _camera;

        private float VerticalAxis => Input.GetAxis("Vertical");
        private float HorizontalAxis => Input.GetAxis("Horizontal");
    
        private float MouseHorizontalAxis => Input.GetAxis("Mouse X");
        private float MouseVerticalAxis => Input.GetAxis("Mouse Y");
        
        public override void Handle()
        {
            HandleMovementInput();
            HandleMouseLook();
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                HandlingPlayer.TryUseSkill<IHackable, Hacking>();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                HandlingPlayer.TryUseSkill<IShootable, Shooting>();
                _camera.Shake();
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                HandlingPlayer.TryAttack();
                _camera.Shake();
            }
        }

        private void HandleMovementInput()
        {
            Transform playerTransform = HandlingPlayer.transform;
            Vector3 motion = playerTransform.forward * VerticalAxis + playerTransform.right * HorizontalAxis;
            
            HandlingPlayer.Move(motion * Time.deltaTime);
        }

        private void HandleMouseLook()
        {
            _camera.RotateVertically(MouseVerticalAxis);
            HandlingPlayer.transform.rotation *= Quaternion.Euler(0, MouseHorizontalAxis * _horizontalLookSpeed, 0);
        }
    }
}