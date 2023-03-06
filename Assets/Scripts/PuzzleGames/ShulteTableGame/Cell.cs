using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShulteTableGame
{
    [RequireComponent(typeof(Image), typeof(Button), typeof(TMP_Text))]
    public class Cell : MonoBehaviour
    {
        private Image _image;
        private Button _button;
        private TMP_Text _text;

        public int Id { get; private set; }

        public event Action<Cell> CellClicked;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();

            _text = GetComponentInChildren<TMP_Text>();
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
            CellClicked?.Invoke(this);
        }

        public void SetColor(Color color)
        {
            _image.color = color;
        }

        public void SetId(int id)
        {
            Id = id;
            _text.SetText(id.ToString());
        }
    }
}