using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Features.Auth
{
    public class AuthorizationMenu : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenu;

        [SerializeField] private SignUpPanel _signUpPanel;
        [SerializeField] private SignInPanel _signInPanel;

        private UserService _userService;

        [Inject]
        private void Construct(UserService userService)
        {
            _userService = userService;
        }

        private void Awake()
        {
            _signUpPanel.SignedUp += Authorize;
            _signInPanel.SignedIn += Authorize;
            _mainMenu.SignedOut += SignOut;
        }

        private void OnDestroy()
        {
            _signUpPanel.SignedUp -= Authorize;
            _signInPanel.SignedIn -= Authorize;
            _mainMenu.SignedOut -= SignOut;
        }

        private void Authorize(User user)
        {
            _userService.CurrentUser = user;
            
            _signUpPanel.gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void SignOut()
        {
            _mainMenu.gameObject.SetActive(false);
            _userService.CurrentUser = null;
            gameObject.SetActive(true);
        }
    }
}