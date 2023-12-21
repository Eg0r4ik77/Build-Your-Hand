using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanel : Panel
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _newPassword;
    [SerializeField] private TMP_InputField _repeatPassword;

    [SerializeField] private Button _changeLoginButton;
    [SerializeField] private Button _changePasswordButton;

    protected override void Enable()
    {
        base.Enable();

        _changeLoginButton.onClick.AddListener(TryChangeLogin);
        _changePasswordButton.onClick.AddListener(TryChangePassword);

        ShowAchievements();
    }

    protected override void Disable()
    {
        base.Disable();

        _changeLoginButton.onClick.RemoveListener(TryChangeLogin);
        _changePasswordButton.onClick.RemoveListener(TryChangePassword);
    }

    private void TryChangeLogin()
    {
        string login = _login.text;
        Debug.Log("Запрос на изменение логина");
    }

    private void TryChangePassword()
    {
        string newPassword = _newPassword.text;
        string repeatedPassword = _repeatPassword.text;

        if (newPassword.Equals(repeatedPassword))
        {
            Debug.Log("Запрос на изменение пароля");
        }
    }

    private void ShowAchievements()
    {
        Debug.Log("Запрос на получение достижений");

    }
}
