using System;
using UnityEngine;
using UnityEngine.UI;

namespace RepeatDrawingGame
{
    public class Cell : MonoBehaviour
    {
        protected Image CellImage;

        public void SetColor(Color color)
        {
            CellImage.color = color;
        }
    }
}