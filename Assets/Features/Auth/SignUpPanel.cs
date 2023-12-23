using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Features.Auth
{
    public class SignUpPanel : Panel
    {
        [SerializeField] private TMP_InputField _login;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private TMP_InputField _repeatedPassword;

        [SerializeField] private Button _signUpButton;

        private AuthorizationService _authorizationService;

        public Action<User> SignedUp; 

        [Inject]
        private void Construct(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        protected override void Enable()
        {
            base.Disable();

            _signUpButton.onClick.AddListener(TrySignUp);
        }

        protected override void Disable()
        {
            base.Disable();

            _signUpButton.onClick.RemoveListener(TrySignUp);
        }

        private async void TrySignUp()
        {
            string login = _login.text;
            string password = _password.text;
            string repeatedPassword = _repeatedPassword.text;

            if (password.Equals(repeatedPassword))
            {
                User user = await _authorizationService.SignUp(login, password);
                SignedUp?.Invoke(user);
            }
            else
            {
                Debug.Log("Пароли не совпадают");
            }
        }
    }
}