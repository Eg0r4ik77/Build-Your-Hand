using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private CursorSwitcher _cursorSwitcher;

    [SerializeField] private FinishPanel _finishPanel;
    [SerializeField] private PausePanel _pausePanel;

    private const string MainMenuSceneName = "MainMenu";

    public Action GameFinished;
    
    private void OnEnable()
    {
        _pausePanel.MainMenuButtonClicked += GoToMainMenu;
        _finishPanel.MainMenuButtonClicked += GoToMainMenu;
        _finishPanel.RestartButtonClicked += Restart;
    }

    private void OnDisable()
    {
        _pausePanel.MainMenuButtonClicked -= GoToMainMenu;
        _finishPanel.MainMenuButtonClicked -= GoToMainMenu;
        _finishPanel.RestartButtonClicked -= Restart;
    }

    private void OnTriggerEnter(Collider other)
    {
        _finishPanel.Open();
        GameFinished?.Invoke();
    }
    
    private void GoToMainMenu()
    {
        Finish();
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private void Restart()
    {
        Finish();

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void Finish()
    {
        _cursorSwitcher.SwitchToCursor();
    }
}
