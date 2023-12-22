using Assets.Features;
using Assets.Features.Auth;
using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProfilePanel : Panel
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _newPassword;
    [SerializeField] private TMP_InputField _repeatPassword;

    [SerializeField] private Button _changeLoginButton;
    [SerializeField] private Button _changePasswordButton;

    [SerializeField] private AchievementsPanel _achievementsPanel;

    protected override void Enable()
    {
        base.Enable();

        SetLoginPlaceholder();

        _changeLoginButton.onClick.AddListener(TryChangeLogin);
        _changePasswordButton.onClick.AddListener(TryChangePassword);

        _achievementsPanel.ShowAchievements("http://localhost:8088/1/achievements/");
    }

    protected override void Disable()
    {
        base.Disable();

        _changeLoginButton.onClick.RemoveListener(TryChangeLogin);
        _changePasswordButton.onClick.RemoveListener(TryChangePassword);

        _achievementsPanel.ClearContent();
    }

    private async void TryChangeLogin()
    {
        string login = _login.text;
        UnityWebRequest request = await UnityWebRequest
            .Put($"http://localhost:8088/1/update-login/{login}", new byte[] {})
            .SendWebRequest()
            .WithCancellation(this.GetCancellationTokenOnDestroy());

        SetLoginPlaceholder();
    }

    private async void TryChangePassword()
    {
        string newPassword = _newPassword.text;
        string repeatedPassword = _repeatPassword.text;

        if (newPassword.Equals(repeatedPassword))
        {
            UnityWebRequest request = await UnityWebRequest
                .Put($"http://localhost:8088/1/update-password/{newPassword}", new byte[] { })
                .SendWebRequest()
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }
        else
        {
            Debug.Log("Пароли не совпадают");
        }
    }

    private void SetLoginPlaceholder()
    {
        // get user login
        ((TMP_Text)_login.placeholder).text = "Login";
    }
}
