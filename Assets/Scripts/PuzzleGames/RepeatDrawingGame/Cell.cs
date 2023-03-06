using UnityEngine;
using UnityEngine.UI;

namespace RepeatDrawingGame
{
    [RequireComponent(typeof(Image))]
    public class Cell : MonoBehaviour
    {
        protected Image CellImage;

        public void SetColor(Color color)
        {
            CellImage.color = color;
        }
    }
}