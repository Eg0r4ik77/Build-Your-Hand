﻿using System;
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
        
        private List<Skill> _skills;
        private ResourcesWallet _wallet;
        
        private Button _selectedButton;
        private List<Image> _purchaseDataButtonImages;

        private int _currentDataIndex;

        private Animator _animator;

        private readonly int AppearanceAnimationHash = Animator.StringToHash("Appearance");
        private readonly int DisappearanceAnimationHash = Animator.StringToHash("Disappearance");
        
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
                new Acceleration()
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

        public void TryPurchase()
        {
            PurchaseData currentData = _dataList[_currentDataIndex];

            if (AvailableForPurchase(currentData))
            {
                Purchase(currentData);
                
                if (_currentDataIndex != _dataList.Count - 1)
                {
                    _currentDataIndex++;
                    
                    Image image = _purchaseDataButtonImages[_currentDataIndex];
                    image.color = _purchasableColor;
                    
                    UpdatePurchaseButtonText();
                }
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
            PurchaseData data = _dataList[_currentDataIndex];
            float cost = data.Cost;
                    
            _purchaseButton.UpdateView(cost);
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
        }

        public void Close()
        {
            StartCoroutine(CloseCoroutine());
        }

        private IEnumerator CloseCoroutine()
        {
            _animator.Play(DisappearanceAnimationHash);
            yield return new WaitForSeconds(.5f);
            _panel.SetActive(false);
        }
    }
}