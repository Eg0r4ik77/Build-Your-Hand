using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Gradient _gradient; 
        [SerializeField] private Image _barImage;
        
        private float _fillAmount;

        private void Start()
        {
            _fillAmount = 1f;
            _barImage.fillAmount = _fillAmount;
            _barImage.color = _gradient.Evaluate(_fillAmount);

            _player.Damaged += UpdateView;
        }

        private void OnDestroy()
        {
            _player.Damaged -= UpdateView;
        }

        private void UpdateView(float health)
        {
            _barImage.fillAmount = health;
            _barImage.color = _gradient.Evaluate(health);
        }
    }
}