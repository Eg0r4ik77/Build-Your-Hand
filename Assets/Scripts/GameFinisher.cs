using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private FinishPanel _finishPanel;
    [SerializeField] private PausePanel _pausePanel;

    private const string MainMenuSceneName = "MainMenu";

    private Pause _pause;
    private CursorSwitcher _cursorSwitcher;
    
    public Action GameFinished;
    
    [Inject]
    private void Construct(Pause pause, CursorSwitcher cursorSwitcher)
    {
        _pause = pause;
        _cursorSwitcher = cursorSwitcher;
    }
    
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
        _pause.SetPaused(true);
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
