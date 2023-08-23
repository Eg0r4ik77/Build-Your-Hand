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
        [SerializeField] private FirstPersonCamera _camera;
        [SerializeField] private Shop _shop;
        [SerializeField] private PauseMenu _pauseMenu;
        
        private UniversalHand Hand => HandlingPlayer.Hand;
        
        private bool IsUseSkillOrAttackInput => Input.GetKeyDown(KeyCode.Mouse0);
        private bool IsOpenShopInput => Input.GetKeyDown(KeyCode.B);
        private bool IsOpenPauseMenuInput => Input.GetKey(KeyCode.Escape);
        
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
            
            if (IsUseSkillOrAttackInput)
            {
                if (HandlingPlayer.HasNoSkills())
                {
                    HandlingPlayer.TryAttack();
                }
                else
                {
                    HandleSkill(Hand.CurrentSkill);
                }
            }

            if (IsOpenShopInput)
            {
                _shop.Open();
            }
            
            if (IsOpenPauseMenuInput)
            {
                _pauseMenu.OpenPausePanel();
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

        private void HandleSkill(Skill skill)
        {
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
}