using System;
using Skills;
using UnityEngine;
using UnityEngine.UI;

namespace Economy
{
    [Serializable]
    public class PurchaseData
    {
        [SerializeField] private Button _skillButton;
        [SerializeField] private float _cost;

        public bool IsPurchased { get; set; }
        public Button SkillButton => _skillButton;
        public float Cost => _cost;

        public Skill PurchasedSkill { get; set; }
    }
}