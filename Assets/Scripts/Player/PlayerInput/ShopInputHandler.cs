using System;
using Economy;
using UnityEngine;

namespace PlayerInput
{
    public class ShopInputHandler : InputHandler
    {
        private Shop _shop;

        private bool IsCloseShopInput => Input.GetKeyDown(KeyCode.B);

        public event Action SwitchedToAction;

        public void SetShop(Shop shop)
        {
            _shop = shop;
        }

        public override void Handle()
        {
            if (IsCloseShopInput)
            {
                _shop.Close();
                SwitchedToAction?.Invoke();
            }
        }
    }
}