using Economy;
using UnityEngine;

public class ShopInputHandler : InputHandler
{
    private readonly Shop _shop;

    private bool IsCloseShopInput => Input.GetKeyDown(KeyCode.B);
    
    public ShopInputHandler(Shop shop, PauseMenu pauseMenu) : base(pauseMenu)
    {
        _shop = shop;
    }

    public override void Handle()
    {
        base.Handle();
        
        if (IsCloseShopInput)
        {
            _shop.Close();
        }
    }
    
    public override void SetCursor(CursorSwitcher cursorSwitcher)
    {
        cursorSwitcher.SwitchToCursor();
    }
}