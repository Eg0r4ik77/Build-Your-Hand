using Assets.Features.Auth;
using Cysharp.Threading.Tasks.Triggers;
using Economy;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Features
{
    public class AchievementsResolver : MonoBehaviour
    {
        private UserService _userService;

        [Inject]
        private void Construct(UserService userService) 
        {
            _userService = userService;
        }

        public async void Initialize()
        {
            List<Achievement> achievements = await _userService.GetAchievements(_userService.CurrentUser);
            
            foreach (Achievement achievement in achievements)
            {
                switch (achievement.Id)
                {
                    case 1:
                        List<Chest> chests = FindObjectsOfType<Chest>().ToList();

                        foreach (Chest chest in chests)
                        {
                            chest.Destroyed += OnFirstChestCrushed;
                        }

                        break;
                }
            }
        }

        private void OnFirstChestCrushed()
        {
            //  запрос
            // отписать все ящики
            // удалить из списка
        }
    }
}