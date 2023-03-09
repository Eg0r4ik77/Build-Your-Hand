using System;
using System.Collections;
using System.Collections.Generic;
using Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    [RequireComponent(typeof(Animator))]
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private List<PurchaseData> _dataList;
        
        [SerializeField] private GameObject _panel;
        [SerializeField] private PurchaseButton _purchaseButton;
        
        [SerializeField] private Color _nonPurchasableColor;
        [SerializeField] private Color _purchasableColor;
        [SerializeField] private Color _purchasedColor;
 
        [SerializeField] private float _shootingDamage = 3f;
        [SerializeField] private float _acceleration = 3f;
        
        private List<Skill> _skills;
        private ResourcesWallet _wallet;
        
        private Button _selectedButton;
        private List<Image> _purchaseDataButtonImages;

        private int _currentDataIndex;

        private Animator _animator;

        private readonly int AppearanceAnimationHash = Animator.StringToHash("Appearance");
        private readonly int DisappearanceAnimationHash = Animator.StringToHash("Disappearance");
        private readonly int IsShopOpenedHash = Animator.StringToHash("Opened");
        
        public event Action<Shop> Opened;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            _purchaseDataButtonImages = new List<Image>();

            foreach (var data in _dataList)
            {
                Button button = data.SkillButton;
                var image = button.GetComponent<Image>();
                
                _purchaseDataButtonImages.Add(image);
            }
            
            // Shop's Start is called before PurchaseButton's Awake =(
            _purchaseButton.PurchaseText = _purchaseButton.GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _skills = new List<Skill>
            {
                new Shooting(_shootingDamage),
                new Hacking(),
                new Acceleration(_acceleration)
            };
            
            _wallet = _player.Wallet;

            for (int i = 0; i<_dataList.Count; i++)
            {
                _dataList[i].PurchasedSkill = _skills[i];
                _purchaseDataButtonImages[i].color = _nonPurchasableColor;
            }

            _purchaseDataButtonImages[0].color = _purchasableColor;
            
            UpdatePurchaseButtonText();
        }
        
        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(TryPurchase);
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.AddListener(TryPurchase);
        }

        private void TryPurchase()
        {
            if (_currentDataIndex == _dataList.Count)
            {
                return;
            }
            
            PurchaseData currentData = _dataList[_currentDataIndex];

            if (AvailableForPurchase(currentData))
            {
                Purchase(currentData);
                if (_currentDataIndex < _dataList.Count - 1)
                { 
                    Image image = _purchaseDataButtonImages[_currentDataIndex + 1];
                    image.color = _purchasableColor;
                }
                _currentDataIndex++;
                UpdatePurchaseButtonText();
            }
        }

        private void Purchase(PurchaseData data)
        {
            Image image = _purchaseDataButtonImages[_currentDataIndex];

            float cost = data.Cost;
            Skill skill = data.PurchasedSkill;
            
            _player.Hand.AddSkill(skill);
            _wallet.Remove(cost);
                
            image.color = _purchasedColor;
            data.IsPurchased = true;
        }
        
        private void UpdatePurchaseButtonText()
        {
            if (_currentDataIndex == _dataList.Count)
            {
                _purchaseButton.OutputCompleted();
            }
            else
            {
                PurchaseData data = _dataList[_currentDataIndex];
                float cost = data.Cost;
                        
                _purchaseButton.UpdateView(cost);
            }
        }

        private bool AvailableForPurchase(PurchaseData data)
        {
            return _wallet.ResourcesSum >= data.Cost && !data.IsPurchased;
        }
        
        public void Open()
        {
            Opened?.Invoke(this);
            _panel.SetActive(true);
            
            _animator.Play(AppearanceAnimationHash);
            _animator.SetBool(IsShopOpenedHash, true);
            
        }

        public void Close()
        {
            _animator.Play(DisappearanceAnimationHash);
            _animator.SetBool(IsShopOpenedHash, false);
        }
    }
}