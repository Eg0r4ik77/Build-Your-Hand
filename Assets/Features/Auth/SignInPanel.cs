using Assets.Features.Auth;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SignInPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;

    [SerializeField] private SignUpPanel _signUpPanel;

    [SerializeField] private Button _signInButton;
    [SerializeField] private Button _signUpButton;

    private AuthorizationService _authorizationService;

    public Action<User> SignedIn;

    [Inject]
    private void Construct(AuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    private void OnEnable()
    {
        _signInButton.onClick.AddListener(TrySignIn);
        _signUpButton.onClick.AddListener(OpenSignUpPanel);
    }

    private void OnDisable()
    {
        _signInButton.onClick.RemoveListener(TrySignIn);
        _signUpButton.onClick.RemoveListener(OpenSignUpPanel);
    }

    private async void TrySignIn()
    {
        string login = _login.text;
        string password = _password.text;

        User user = await _authorizationService.SignIn(login, password);
        SignedIn?.Invoke(user);
    }

    private void OpenSignUpPanel()
    {
        _signUpPanel.Open();
    }
}
