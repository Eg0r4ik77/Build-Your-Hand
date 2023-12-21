using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Features.Auth
{
    public class UserInfoPanel : Panel
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _ratingText;
        [SerializeField] private AchievementsPanel _achievementsPanel;

        private User _user;

        public void SetUser(User user)
        {
            _user = user;
        }

        protected override void Enable()
        {
            base.Enable();

            _nameText.text = _user.Login;
            _ratingText.text = "100";
            _achievementsPanel.ShowAchievements($"http://localhost:8088/{_user.Id}/achievements/");
        }

        protected override void Disable()
        {
            base.Disable();

            _achievementsPanel.ClearContent();
        }
    }
}