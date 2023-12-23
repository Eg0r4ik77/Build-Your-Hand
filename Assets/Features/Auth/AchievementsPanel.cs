using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Features.Auth
{
    internal class AchievementsPanel : MonoBehaviour
    {
        [SerializeField] private AchievementView _achievementViewPrefab;

        private RectTransform _rectTransform;
        private List<AchievementView> _achievementViews = new();

        private UserService _userService;

        [Inject]
        private void Construct(UserService userService)
        {
            _userService = userService;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void ShowCurrentUserAchievements()
        {
            ShowAchievements(_userService.CurrentUser);
        }

        public async void ShowAchievements(User user)
        {
            List<Achievement> achievements = await _userService.GetAchievements(user);

            ShowAchievements(achievements);
        }

        public void ClearContent()
        {
            _achievementViews?.ForEach(achievementView => Destroy(achievementView.gameObject));
            _achievementViews = new List<AchievementView>();
        }

        private void ShowAchievements(List<Achievement> achievements)
        {
            if (achievements == null)
            {
                Debug.Log("No achievements");
                return;
            }

            foreach (Achievement achievement in achievements)
            {
                AchievementView view = Instantiate(_achievementViewPrefab, _rectTransform);
                view.Name = achievement.Name;
                view.Description = achievement.Description;
                view.Date = achievement.Date;
                _achievementViews.Add(view);
            }
        }
    }
}
