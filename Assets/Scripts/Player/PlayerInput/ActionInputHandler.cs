using System;
using Economy;
using Movement;
using PlayerCamera;
using Skills;
using UnityEngine;

namespace PlayerInput
{
    [Serializable]
    public class ActionInputHandler : InputHandler
    {
        [SerializeField] private FirstPersonCamera _camera;
        [SerializeField] private Shop _shop;

        private UniversalHand Hand => HandlingPlayer.Hand;

        private float VerticalAxis => Input.GetAxis("Vertical");
        private float HorizontalAxis => Input.GetAxis("Horizontal");
        private float MouseHorizontalAxis => Input.GetAxis("Mouse X");
        private float MouseVerticalAxis => Input.GetAxis("Mouse Y");
        private float MouseScrollWheel => Input.GetAxis("Mouse ScrollWheel");

        public override void Handle()
        {
            HandleMovementInput();
            HandleMouseLook();
            
            Hand.SwitchSkill(MouseScrollWheel);
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (HandlingPlayer.HasNoSkills())
                {
                    HandlingPlayer.TryAttack();
                }
                else
                {
                    Skill skill = Hand.CurrentSkill;
                    if (skill is Shooting or Hacking)
                    {
                        bool result = Hand.TryUseCurrentSkill();
                    
                        if (skill is Shooting && result)
                        {
                            _camera.Shake();                    
                        }   
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.B))
            {
                _shop.Open();
            }
            
            Hand.TryUseAcceleration();
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
            HandlingPlayer.RotateHorizontally(MouseHorizontalAxis);
        }
    }
}