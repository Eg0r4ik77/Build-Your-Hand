using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Features.Auth
{
    public class AuthorizationMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;

        [SerializeField] private SignUpPanel _signUpPanel;
        [SerializeField] private SignInPanel _signInPanel;

        private UserService _userService;

        [Inject]
        private void Construct(UserService userService)
        {
            _userService = userService;
        }

        private void OnEnable()
        {
            _signUpPanel.SignedUp += Authorize;
            _signInPanel.SignedIn += Authorize;
        }

        private void OnDisable()
        {
            _signUpPanel.SignedUp -= Authorize;
            _signInPanel.SignedIn -= Authorize;
        }

        private void Authorize(User user)
        {
            _userService.CurrentUser = user;
            
            _signUpPanel.gameObject.SetActive(false);
            _menu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}