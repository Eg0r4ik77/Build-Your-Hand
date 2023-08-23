using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{    
    [SerializeField] private Button _resumeGameButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _mainMenuButton;
    
    [SerializeField] private Panel _howToPlayPanel;
    
    private const string MainMenuSceneName = "MainMenu";

    public Action PauseCancelled;

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(MainMenu);
        _resumeGameButton.onClick.AddListener(ResumeGame);
        _howToPlayButton.onClick.AddListener(_howToPlayPanel.Open);
    }
    
    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _resumeGameButton.onClick.RemoveListener(ResumeGame);
        _howToPlayButton.onClick.RemoveListener(_howToPlayPanel.Open);
    }

    private void ResumeGame()
    {
        CancelPause();
    }

    private void MainMenu()
    {
        CancelPause();
        SceneManager.LoadScene(MainMenuSceneName);
    }

    private void CancelPause()
    {
        PauseCancelled?.Invoke();
    }
}