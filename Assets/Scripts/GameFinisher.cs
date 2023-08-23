using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private InputHandlersSwitcher _inputHandlersSwitcher;
    [SerializeField] private MainMenu _finishPanel;
    [SerializeField] private CursorSwitcher _cursorSwitcher;

    private void OnTriggerEnter(Collider other)
    {
        _finishPanel.gameObject.SetActive(true);
        _cursorSwitcher.SwitchPredictionPointToCursor();
        _inputHandlersSwitcher.gameObject.SetActive(false);
    }
}
