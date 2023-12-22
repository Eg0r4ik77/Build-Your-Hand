using Assets.Features.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UserView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _ratingText;

    private User _user;
    private UserInfoPanel _userInfoPanel;

    private UserService _userService;

    public void Initialize(User user, UserService userService, UserInfoPanel userInfoPanel)
    {
        _userService = userService;

        _user = user;
        _userInfoPanel = userInfoPanel;

        _nameText.text = user.Login;

        if(user.Id == _userService.CurrentUser.Id)
        {
            _nameText.text += " (You)";
        }

        _ratingText.text = _userService.Rating(user).ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _userInfoPanel.SetUser(_user);
        _userInfoPanel.Open();
    }
}