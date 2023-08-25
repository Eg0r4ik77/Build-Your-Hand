using System;
using UnityEngine;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PausePanel _panel;

    private Pause _pause;
    
    public Action Opened;
    public Action Closed;

    [Inject]
    private void Construct(Pause pause)
    {
        _pause = pause;
    }
    
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
        
        _pause.SetPaused(true);
    }

    public void ClosePausePanel()
    {
        Closed?.Invoke();
        _panel.gameObject.SetActive(false);
        
        _pause.SetPaused(false);
    }
}
