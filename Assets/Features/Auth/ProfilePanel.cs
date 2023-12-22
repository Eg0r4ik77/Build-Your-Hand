using Assets.Features.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ProfilePanel : Panel
{
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _newPassword;
    [SerializeField] private TMP_InputField _repeatPassword;

    [SerializeField] private Button _changeLoginButton;
    [SerializeField] private Button _changePasswordButton;

    [SerializeField] private AchievementsPanel _achievementsPanel;

    private UserService _userService;
    private TextMeshProUGUI _placeholderText;

    [Inject]
    private void Construct(UserService userService)
    {
        _userService = userService;
    }

    private void Awake()
    {
        _placeholderText = _login.placeholder.GetComponent<TextMeshProUGUI>();
    }

    protected override void Enable()
    {
        base.Enable();

        SetLoginPlaceholder();

        _changeLoginButton.onClick.AddListener(TryUpdateLogin);
        _changePasswordButton.onClick.AddListener(TryUpdatePassword);

        _achievementsPanel.ShowCurrentUserAchievements();
    }

    protected override void Disable()
    {
        base.Disable();

        _changeLoginButton.onClick.RemoveListener(TryUpdateLogin);
        _changePasswordButton.onClick.RemoveListener(TryUpdatePassword);

        _achievementsPanel.ClearContent();
    }

    private void TryUpdateLogin()
    {
        string login = _login.text;
        _userService.TryUpdateLogin(login);
        SetLoginPlaceholder();
    }

    private void TryUpdatePassword()
    {
        string newPassword = _newPassword.text;
        string repeatedPassword = _repeatPassword.text;

        if (newPassword.Equals(repeatedPassword))
        {
            _userService.TryUpdatePassword(newPassword);
        }
        else
        {
            Debug.Log("Пароли не совпадают");
        }
    }

    private void SetLoginPlaceholder()
    {
        string login = _userService.CurrentUser.Login;
        _placeholderText.text = login;
    }
}
