using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Device.Application;

namespace Scenario
{
    public class MenuManager : MonoBehaviour
    {
        private const int MenuBuildIndex = 0;
        private const int GameLevelBuildIndex = 1;
        
        public void StartGame()
        {
            SceneManager.LoadScene(GameLevelBuildIndex);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(MenuBuildIndex);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}