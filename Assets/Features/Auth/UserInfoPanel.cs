using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Features.Auth
{
    public class UserInfoPanel : Panel
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _ratingText;
        [SerializeField] private AchievementsPanel _achievementsPanel;

        [SerializeField] private Button _switchSubscriptionButton;
        [SerializeField] private TMP_Text _switchSubscriptionText;

        private User _user;
        private bool _isFollowing;

        private UserService _userService;

        public void Initialize(UserService userService)
        {
            _userService = userService;
        }

        public void SetUser(User user)
        {
            _user = user;
        }

        protected override void Enable()
        {
            base.Enable();

            _nameText.text = _user.Login;
            SetRatingText(_user);

            SwitchFollowingState();
            _achievementsPanel.ShowAchievements(_user);

            _switchSubscriptionButton.onClick.AddListener(SwitchSubscription);
        }

        protected override void Disable()
        {
            base.Disable();

            _achievementsPanel.ClearContent();
            _switchSubscriptionButton.onClick.RemoveListener(SwitchSubscription);
        }

        private void SwitchSubscription()
        {
            if (_isFollowing)
            {
                _userService.UnsubscribeCurrentUser(_user.Id);  
            }
            else
            {
                _userService.SubscribeCurrentUser(_user.Id);
            }

            SwitchFollowingState();
        }

        private async void SwitchFollowingState()
        {
            List<User> followings = await _userService.GetFollowings();

            _isFollowing = followings.Exists(following => following.Id == _user.Id);

            _switchSubscriptionText.text = _isFollowing
                ? "Unsubscribe"
                : "Subscribe";
        }

        public async void SetRatingText(User user)
        {
            int rating = await _userService.GetRating(user);
            await UniTask.SwitchToMainThread();
            _ratingText.text = rating.ToString();
        }
    }
}