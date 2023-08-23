using System;
using Economy;
using PlayerCamera;
using Skills;
using UnityEngine;

namespace PlayerInput
{
    public class ActionInputHandler : InputHandler
    {
        private readonly Player _player;
        private readonly Shop _shop;
        private readonly PauseMenu _pauseMenu;
        
        private FirstPersonCamera Camera => _player.Camera;
        private UniversalHand Hand => _player.Hand;
        
        private bool IsUseSkillOrAttackInput => Input.GetKeyDown(KeyCode.Mouse0);
        private bool IsOpenShopInput => Input.GetKeyDown(KeyCode.B);
        private bool IsOpenPauseMenuInput => Input.GetKey(KeyCode.Escape);
        
        private float VerticalAxis => Input.GetAxis("Vertical");
        private float HorizontalAxis => Input.GetAxis("Horizontal");
        private float MouseHorizontalAxis => Input.GetAxis("Mouse X");
        private float MouseVerticalAxis => Input.GetAxis("Mouse Y");
        private float MouseScrollWheel => Input.GetAxis("Mouse ScrollWheel");

        public ActionInputHandler(Player player, Shop shop, PauseMenu pauseMenu)
        {
            _player = player;
            _shop = shop;
            _pauseMenu = pauseMenu;
        }

        public void Handle()
        {
            HandleMovementInput();
            HandleMouseLook();
            
            Hand.SwitchSkill(MouseScrollWheel);
            
            if (IsUseSkillOrAttackInput)
            {
                if (_player.HasNoSkills())
                {
                    _player.TryAttack();
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
            Transform playerTransform = _player.transform;
            Vector3 motion = playerTransform.forward * VerticalAxis + playerTransform.right * HorizontalAxis;
            
            _player.Move(motion * Time.deltaTime);
        }

        private void HandleMouseLook()
        {
            Camera.RotateVertically(MouseVerticalAxis);
            _player.RotateHorizontally(MouseHorizontalAxis);
        }

        private void HandleSkill(Skill skill)
        {
            if (skill is Shooting or Hacking)
            {
                bool result = Hand.TryUseCurrentSkill();
                    
                if (skill is Shooting && result)
                {
                    Camera.Shake();                    
                }   
            }
        }
    }
}