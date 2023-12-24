using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _profilePanelButton;
    [SerializeField] private Button _usersPanelButton;
    [SerializeField] private Button _signOutButton;

    [SerializeField] private Panel _profilePanel;
    [SerializeField] private Panel _usersPanel;

    private const string GameSceneName = "Game";
    private List<Panel> _panels;

    public event Action SignedOut;

    private void Start()
    {
        _panels = new()
        {
            _profilePanel,
            _usersPanel
        };
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(ExitGame);
        _signOutButton.onClick.AddListener(SignOut);

        _profilePanelButton.onClick.AddListener(ShowProfilePanel);
        _usersPanelButton.onClick.AddListener(ShowUsersPanel);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(StartGame);
        _exitButton.onClick.RemoveListener(ExitGame);
        _signOutButton.onClick.RemoveListener(SignOut);

        _profilePanelButton.onClick.RemoveListener(ShowProfilePanel);
        _usersPanelButton.onClick.RemoveListener(ShowUsersPanel);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void ShowProfilePanel()
    {
        _panels.ForEach(panel => panel.Close());
        _profilePanel.Open();
    }

    private void ShowUsersPanel()
    {
       ClosePanels();
        _usersPanel.Open();
    }

    private void SignOut()
    {
        ClosePanels();
        SignedOut?.Invoke();
    }

    private void ClosePanels()
    {
        _panels.ForEach(panel => panel.Close());
    }
}
