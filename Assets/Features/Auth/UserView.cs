using Assets.Features.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _ratingText;
    
    private User _user;
    private UserInfoPanel _userInfoPanel;

    public void Initialize(User user, UserInfoPanel userInfoPanel)
    {
        _user = user;
        _userInfoPanel = userInfoPanel;

        _nameText.text = user.Login;
        _ratingText.text = "100";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _userInfoPanel.SetUser(_user);
        _userInfoPanel.Open();
    }
}