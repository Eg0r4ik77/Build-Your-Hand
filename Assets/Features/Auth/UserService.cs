using Cysharp.Threading.Tasks;
using ModestTree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;


namespace Assets.Features.Auth
{
    public class UserService : MonoBehaviour
    {
        public User CurrentUser { get; set; }

        public async Task<List<User>> GetAllUsers()
            => await GetUsers("http://localhost:8088/users");

        public async Task<List<User>> GetFollowings()
            => await GetUsers($"http://localhost:8088/{CurrentUser.Id}/followings");

        public async Task<List<User>> GetFollowers()
            => await GetUsers($"http://localhost:8088/{CurrentUser.Id}/followers");

        public async Task<List<User>> GetUsers(string uri)
            => await GetListByRequest<User>(uri);

        public async UniTask<List<Achievement>> GetAchievements(User user)
            => await GetListByRequest<Achievement>($"http://localhost:8088/{user.Id}/achievements/");

        public async void TryUpdateLogin(string login)
        {
            UnityWebRequest request = await UnityWebRequest
                .Put($"http://localhost:8088/{CurrentUser.Id}/update-login/{login}", new byte[] { })
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }

        public async void TryUpdatePassword(string password)
        {
            UnityWebRequest request = await UnityWebRequest
                .Put($"http://localhost:8088/{CurrentUser.Id}/update-password/{password}", new byte[] { })
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }

        public async void SubscribeCurrentUser(int userId)
        {
            await UnityWebRequest
                .Post($"http://localhost:8088/{CurrentUser.Id}/subscribe/{userId}", "")
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }

        public async void UnsubscribeCurrentUser(int userId)
        {
            await UnityWebRequest
                .Delete($"http://localhost:8088/{CurrentUser.Id}/unsubscribe/{userId}")
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }

        public async UniTask<int> GetRating(User user)
        {
            UnityWebRequest request = await UnityWebRequest
                .Get($"http://localhost:8088/{user.Id}/rating")
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());

            string json = request.downloadHandler.text;

            int rating = json.IsEmpty()
                ? 0
                : int.Parse(json);

            return rating;
        }

        private async UniTask<List<T>> GetListByRequest<T>(string uri)
        {
            UnityWebRequest request = await UnityWebRequest
                .Get(uri)
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());

            string json = request.downloadHandler.text;
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json);

            return list;
        }
    }
}