using System;
using UnityEngine;
using UnityEngine.UI;

namespace FindPairsGame
{
    public class Cell : MonoBehaviour
    {
        private Image _image;
        private Button _button;

        public Color CellColor { get; set; }
        public int Id { get; set; }

        public event Action<Cell> CellClicked;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnCellClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnCellClicked);
        }
        
        private void OnCellClicked()
        {
            ShowCellColor();
            CellClicked?.Invoke(this);
        }

        public void SetClickable(bool clickable)
        {
            _image.raycastTarget = clickable;
        }
        
        private void ShowCellColor()
        {
            _image.color = CellColor;
        }

        public void ShowColor(Color color)
        {
            _image.color = color;
        }
    }
}