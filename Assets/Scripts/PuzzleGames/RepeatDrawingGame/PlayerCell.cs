using System;
using UnityEngine;
using UnityEngine.UI;

namespace RepeatDrawingGame
{
    [RequireComponent(typeof(Button))]
    public class PlayerCell : Cell
    {
        private Button _button;
        
        public bool Clicked { get; private set; }
        public int Index { get; set; }

        public event Action<int> CellClicked;
        
        private void Awake()
        {
            CellImage = GetComponent<Image>();
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
            Clicked = !Clicked;
            CellClicked?.Invoke(Index);
        }
    }
}