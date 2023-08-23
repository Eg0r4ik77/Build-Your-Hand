using Economy;
using PlayerCamera;
using Skills;
using UnityEngine;

public class ActionInputHandler : InputHandler
{
    private readonly Player _player;
    private readonly Shop _shop;
    
    private FirstPersonCamera Camera => _player.Camera;
    private UniversalHand Hand => _player.Hand;
    
    private bool IsUseSkillOrAttackInput => Input.GetKeyDown(KeyCode.Mouse0);
    private bool IsOpenShopInput => Input.GetKeyDown(KeyCode.B);
    private float VerticalAxis => Input.GetAxis("Vertical");
    private float HorizontalAxis => Input.GetAxis("Horizontal");
    private float MouseHorizontalAxis => Input.GetAxis("Mouse X");
    private float MouseVerticalAxis => Input.GetAxis("Mouse Y");
    private float MouseScrollWheel => Input.GetAxis("Mouse ScrollWheel");

    public ActionInputHandler(Player player, Shop shop, PauseMenu pauseMenu) : base(pauseMenu)
    {
        _player = player;
        _shop = shop;
    }

    public override void Handle()
    {
        base.Handle();
        
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

        Hand.TryUseAcceleration();
    }

    public override void SetCursor(CursorSwitcher cursorSwitcher)
    { 
        cursorSwitcher.SwitchToPredictionPoint();
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