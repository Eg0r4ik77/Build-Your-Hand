using System;
using Economy;
using PlayerCamera;
using Skills;
using UnityEngine;

namespace PlayerInput
{
    [Serializable]
    public class ActionInputHandler : InputHandler
    {
        [SerializeField, Range(1, 10)] private float _horizontalLookSpeed = 2f;
        
        [SerializeField] private FirstPersonCamera _camera;
        
        [SerializeField] private Shop _shop;

        private UniversalHand Hand => HandlingPlayer.Hand;

        private float VerticalAxis => Input.GetAxis("Vertical");
        private float HorizontalAxis => Input.GetAxis("Horizontal");
    
        private float MouseHorizontalAxis => Input.GetAxis("Mouse X");
        private float MouseVerticalAxis => Input.GetAxis("Mouse Y");

        public override void Handle()
        {
            HandleMovementInput();
            HandleMouseLook();
            
            Hand.SwitchSkill(Input.GetAxis("Mouse ScrollWheel"));
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (HandlingPlayer.HasNoSkills())
                {
                    HandlingPlayer.TryAttack();
                }
                else
                {
                    bool result = Hand.TryUseCurrentSkill();
                    
                    if (Hand.CurrentSkill is Shooting && result)
                    {
                        _camera.Shake();                    
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.B))
            {
                _shop.Open();
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