using System;
using UnityEngine;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{    
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    
    public Action MainMenuButtonClicked;
    public Action RestartButtonClicked;

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(GoMainMenu);
        _restartButton.onClick.AddListener(RestartGame);
    }
    
    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(GoMainMenu);
        _restartButton.onClick.RemoveListener(RestartGame);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    
    private void RestartGame()
    {
        RestartButtonClicked?.Invoke();
    }
    
    private void GoMainMenu()
    {
        MainMenuButtonClicked?.Invoke();
    }
}