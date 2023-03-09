using GameState;
using PlayerInput;
using Scenario;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    [SerializeField] private GameBehaviour _behaviour;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private MenuManager _finishPanel;

    private void OnTriggerEnter(Collider other)
    {
        _finishPanel.gameObject.SetActive(true);
        _behaviour.SwitchState<StandState>();
        _inputManager.gameObject.SetActive(false);
    }
}
