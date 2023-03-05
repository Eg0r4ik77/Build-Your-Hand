using UnityEngine.UI;

namespace RepeatDrawingGame
{
    public class TemplateCell : Cell
    {
        public bool Id { get; set; }
        
        private void Awake()
        {
            CellImage = GetComponent<Image>();
        }

    }
}