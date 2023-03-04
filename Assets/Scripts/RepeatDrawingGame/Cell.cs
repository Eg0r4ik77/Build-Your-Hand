using System;
using UnityEngine;
using UnityEngine.UI;

namespace RepeatDrawingGame
{
    public class Cell : MonoBehaviour
    {
        private Image _image;
        private Button _button;

        public bool Id { get; set; }
        public bool Clicked { get; set; }
        public int Index { get; set; }

        public event Action<int> CellClicked;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if(_button)
                _button.onClick.AddListener(OnCellClicked);
        }

        private void OnDisable()
        {
            if(_button)
                _button.onClick.RemoveListener(OnCellClicked);
        }
        
        private void OnCellClicked()
        {
            CellClicked?.Invoke(Index);
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}