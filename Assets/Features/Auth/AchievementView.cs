using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Features.Auth
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _dateText;

        public string Name
        {
            get => _nameText.text;
            set => _nameText.text = value;
        }

        public string Description
        {
            get => _descriptionText.text;
            set => _descriptionText.text = value;
        }

        public string Date
        {
            get => _dateText.text;
            set => _dateText.text = value;
        }
    }
}