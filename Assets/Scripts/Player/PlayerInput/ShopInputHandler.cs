using System;
using Economy;
using UnityEngine;

namespace PlayerInput
{
    public class ShopInputHandler : InputHandler
    {
        private Shop _shop;
        public event Action SwitchedToAction;

        public void SetShop(Shop shop)
        {
            _shop = shop;
        }

        public override void Handle()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                _shop.Close();
                SwitchedToAction?.Invoke();
            }
        }
    }
}