using TMPro;
using UnityEngine.UI;

namespace Economy
{
    public class PurchaseButton : Button
    {
        private const string Text = "Купить: ";
        
        public TMP_Text PurchaseText { private get; set; }

        public void UpdateView(float cost)
        {
            if (PurchaseText)
            {
                PurchaseText.text = Text + cost;                
            }
        }
    }
}