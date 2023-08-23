using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PausePanel _panel;

    public Action Opened;

    private void OnEnable()
    {
        _panel.PauseCancelled += ClosePausePanel;
    }

    private void OnDisable()
    {
        _panel.PauseCancelled -= ClosePausePanel;
    }

    public void OpenPausePanel()
    {
        _panel.gameObject.SetActive(true);
        Time.timeScale = 0;
        Opened?.Invoke();
    }

    private void ClosePausePanel()
    {
        _panel.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
}
