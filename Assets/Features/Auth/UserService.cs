using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;


namespace Assets.Features.Auth
{
    public class UserService : MonoBehaviour, IInitializable
    {
        public User CurrentUser { get; set; }

        public void Initialize()
        {
            GetCurrentUser();
        }

        private async void GetCurrentUser()
        {
            var allUsers = await GetAllUsers();
            CurrentUser = allUsers[0];
        }

        public int Rating(User user)
        {
            

            return 0;
        }

        public async UniTask<List<User>> GetAllUsers()
            => await GetUsers("http://localhost:8088/users");

        public async UniTask<List<User>> GetFollowings()
            => await GetUsers($"http://localhost:8088/{CurrentUser.Id}/followings");

        public async UniTask<List<User>> GetFollowers()
            => await GetUsers($"http://localhost:8088/{CurrentUser.Id}/followers");

        public async UniTask<List<User>> GetUsers(string uri)
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