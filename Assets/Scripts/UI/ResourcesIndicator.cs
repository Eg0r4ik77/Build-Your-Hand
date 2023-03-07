using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ResourcesIndicator : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private TMP_Text _tmpText;

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            _player.Wallet.Changed += UpdateView;
        }

        private void OnDestroy()
        {
            _player.Wallet.Changed -= UpdateView;
        }

        private void UpdateView(float resources)
        {
            _tmpText.text = $"Resources: {resources}";
        }
    }
}