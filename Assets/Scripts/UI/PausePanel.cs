using System;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{    
    [SerializeField] private Button _resumeGameButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _mainMenuButton;
    
    [SerializeField] private Panel _howToPlayPanel;
    
    public Action PauseCancelled;
    public Action MainMenuButtonClicked;

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(GoToMainMenu);
        _resumeGameButton.onClick.AddListener(ResumeGame);
        _howToPlayButton.onClick.AddListener(_howToPlayPanel.Open);
    }
    
    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(GoToMainMenu);
        _resumeGameButton.onClick.RemoveListener(ResumeGame);
        _howToPlayButton.onClick.RemoveListener(_howToPlayPanel.Open);
    }

    private void ResumeGame()
    {
        CancelPause();
    }

    private void GoToMainMenu()
    {
        CancelPause();
        MainMenuButtonClicked?.Invoke();
    }

    private void CancelPause()
    {
        PauseCancelled?.Invoke();
    }
}