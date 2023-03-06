using System;
using Doors;
using PuzzleGames;
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

        public event Action<PuzzleGame> SwitchedToPuzzle;
        
        public override void Handle()
        {
            HandleMovementInput();
            HandleMouseLook();

            Skill hacking = HandlingPlayer.TryGetSkill(Skill.Hacking);
            
            if (Input.GetKeyDown(KeyCode.E) && hacking != Skill.Null)
            {
                ISkillTarget target = TryGetSkillTarget();

                if (target is HackableDoor door && HandlingPlayer.TryUseSkill(target, hacking))
                {
                    PuzzleGame game = door.Game;
                    SwitchedToPuzzle?.Invoke(game);
                }
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

        private ISkillTarget TryGetSkillTarget()
        {
            ISkillTarget target = null;

            if (Physics.Raycast(HandlingPlayer.transform.position, HandlingPlayer.transform.forward, out RaycastHit hit, 4f))
            {
                target = hit.transform.GetComponent<ISkillTarget>();
            }
            
            return target;
        }
    }
}