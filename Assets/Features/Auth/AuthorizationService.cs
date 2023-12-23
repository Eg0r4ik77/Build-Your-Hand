using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Assets.Features.Auth
{
    public class AuthorizationService : MonoBehaviour
    {
        public async UniTask<User> SignUp(string login, string password)
        {
            UnityWebRequest request = await UnityWebRequest
                .Post($"http://localhost:8088/sign-up/{login}/{password}", "")
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());

            string json = request.downloadHandler.text;
            User user = JsonConvert.DeserializeObject<User>(json);

            return user;
        }

        public async UniTask<User> SignIn(string login, string password)
        {
            UnityWebRequest request = await UnityWebRequest
                .Post($"http://localhost:8088/sign-in/{login}/{password}", "")
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());


            string json = request.downloadHandler.text;
            User user = JsonConvert.DeserializeObject<User>(json);

            return user;
        }
    }
}
