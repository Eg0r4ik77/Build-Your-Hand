using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

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

        public void SetUser(User user)
        {
            _user = user;
        }

        protected override void Enable()
        {
            base.Enable();

            _nameText.text = _user.Login;
            _ratingText.text = "100";
            SwitchFollowingState();
            _achievementsPanel.ShowAchievements($"http://localhost:8088/{_user.Id}/achievements/");
            _switchSubscriptionButton.onClick.AddListener(SwitchSubscription);
        }

        protected override void Disable()
        {
            base.Disable();

            _achievementsPanel.ClearContent();
            _switchSubscriptionButton.onClick.RemoveListener(SwitchSubscription);
        }

        private async void SwitchSubscription()
        {
            if (_isFollowing)
            {
                await UnityWebRequest
                    .Delete($"http://localhost:8088/1/unsubscribe/{_user.Id}")
                    .SendWebRequest()
                    .WithCancellation(this.GetCancellationTokenOnDestroy());
            }
            else
            {
                await UnityWebRequest
                    .Post($"http://localhost:8088/1/subscribe/{_user.Id}", "")
                    .SendWebRequest()
                    .WithCancellation(this.GetCancellationTokenOnDestroy());
            }

            SwitchFollowingState();
        }

        private async void SwitchFollowingState()
        {
            UnityWebRequest request = await UnityWebRequest
                .Get("http://localhost:8088/1/followings")
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());

            string usersJson = request.downloadHandler.text;

            Debug.Log($"{usersJson}");
            List<User> followings = JsonConvert.DeserializeObject<List<User>>(usersJson);

            _isFollowing = followings.Exists(following => following.Id == _user.Id);

            _switchSubscriptionText.text = _isFollowing
                ? "Unsubscribe"
                : "Subscribe";
        }
    }
}