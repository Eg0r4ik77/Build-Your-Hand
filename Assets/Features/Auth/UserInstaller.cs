using UnityEngine;
using Zenject;

namespace Assets.Features.Auth
{
    public class UserInstaller : MonoInstaller
    {
        [SerializeField] private UserService _userService;
        [SerializeField] private AuthorizationService _authorizationService;

        public override void InstallBindings()
        {
            Container
                .Bind<UserService>()
                .FromInstance(_userService)
                .AsSingle();

            Container
                .Bind<AuthorizationService>()
                .FromInstance(_authorizationService)
                .AsSingle();
        }   
    }
}