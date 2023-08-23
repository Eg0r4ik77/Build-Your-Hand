using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PausePanel _panel;

    public Action Opened;
    public Action Closed;

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
        Opened?.Invoke();
        _panel.gameObject.SetActive(true);
        
        Pause.Instance.SetPaused(true);
    }

    public void ClosePausePanel()
    {
        Closed?.Invoke();
        _panel.gameObject.SetActive(false);
        
        Pause.Instance.SetPaused(false);
    }
}
