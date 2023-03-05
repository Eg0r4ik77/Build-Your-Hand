using System;
using UnityEngine;
using UnityEngine.UI;

namespace FindPairsGame
{
    [RequireComponent(typeof(Image), typeof(Button))]
    public class Cell : MonoBehaviour
    {
        private Image _image;
        private Button _button;

        private Color _cellColor;

        public int Id { get; private set; }

        public event Action<Cell> Clicked;

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
        
        public void Initialize(int id, Color color)
        {
            Id = id;
            _cellColor = color;
        }
        
        private void OnCellClicked()
        {
            SetColor(_cellColor);
            Clicked?.Invoke(this);
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
        
        public void SetClickable(bool clickable)
        {
            _image.raycastTarget = clickable;
        }
    }
}