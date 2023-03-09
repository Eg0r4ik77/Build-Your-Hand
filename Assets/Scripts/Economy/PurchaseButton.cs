using TMPro;
using UnityEngine.UI;

namespace Economy
{
    public class PurchaseButton : Button
    {
        private const string Text = "Purchase: ";
        
        public TMP_Text PurchaseText { private get; set; }

        public void UpdateView(float cost)
        {
            if (PurchaseText)
            {
                PurchaseText.text = Text + cost;                
            }
        }

        public void OutputCompleted()
        {
            if (PurchaseText)
            {
                PurchaseText.text = "Completed";                
            }
        }
    }
}