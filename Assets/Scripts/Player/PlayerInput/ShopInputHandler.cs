using System;
using Economy;
using UnityEngine;

namespace PlayerInput
{
    public class ShopInputHandler : InputHandler
    {
        private readonly Shop _shop;

        private bool IsCloseShopInput => Input.GetKeyDown(KeyCode.B);
        
        public ShopInputHandler(Shop shop)
        {
            _shop = shop;
        }

        public void Handle()
        {
            if (IsCloseShopInput)
            {
                _shop.Close();
            }
        }
    }
}