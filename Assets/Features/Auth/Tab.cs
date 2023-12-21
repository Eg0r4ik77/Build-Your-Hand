using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Features.Auth
{
    public class Tab : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _isSelectedColor = Color.white;

        private Image _image;

        public Action<Tab> Clicked;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
            Clicked?.Invoke(this);
        }

        public void Select()
        {
            _image.color = _isSelectedColor;
        }

        public void SetDefaultColor()
        {
            _image.color = _defaultColor;
        }
    }
}