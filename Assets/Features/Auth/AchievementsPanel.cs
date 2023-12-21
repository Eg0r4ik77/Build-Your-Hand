using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Features.Auth
{
    internal class AchievementsPanel : MonoBehaviour
    {
        [SerializeField] private AchievementView _achievementViewPrefab;

        private RectTransform _rectTransform;
        private List<AchievementView> _achievementViews = new();

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public async void ShowAchievements(string uri)
        {
            List<Achievement> achievements = await GetAchievements(uri);
            ShowAchievements(achievements);
        }

        public void ClearContent()
        {
            _achievementViews?.ForEach(achievementView => Destroy(achievementView.gameObject));
            _achievementViews = new List<AchievementView>();
        }

        private void ShowAchievements(List<Achievement> achievements)
        {
            foreach (Achievement achievement in achievements)
            {
                AchievementView view = Instantiate(_achievementViewPrefab, _rectTransform);
                view.Name = achievement.Name;
                view.Description = achievement.Description;
                view.Date = achievement.Date;
                _achievementViews.Add(view);
            }
        }

        private async UniTask<List<Achievement>> GetAchievements(string uri)
        {
            UnityWebRequest request = await UnityWebRequest
                .Get(uri)
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());

            string achievementsJson = request.downloadHandler.text;

            Debug.Log($"{achievementsJson}");
            List<Achievement> achievements = JsonConvert.DeserializeObject<List<Achievement>>(achievementsJson);

            return achievements;
        }
    }
}
