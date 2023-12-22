using UnityEngine;
using Zenject;

namespace Assets.Features.Auth
{
    public class UserInstaller : MonoInstaller
    {
        [SerializeField] private UserService _userService;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<UserService>()
                .FromInstance(_userService)
                .AsSingle();
        }   
    }
}