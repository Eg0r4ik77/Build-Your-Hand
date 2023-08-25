using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> _checkpoints;

    private Player _player;
    
    private Vector3 _respawnPlayerPosition;
    private Quaternion _respawnPlayerRotation;
    private Quaternion _respawnCameraRotation;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }
    
    private void Start()
    {
        OnChecked();
    }

    private void OnEnable()
    {
        _player.Died += Respawn;

        foreach (Checkpoint checkpoint in _checkpoints)
        {
            checkpoint.Checked += OnChecked;
        }
    }

    private void OnDisable()
    {
        _player.Died -= Respawn;
        
        foreach (Checkpoint checkpoint in _checkpoints)
        {
            checkpoint.Checked -= OnChecked;
        }
    }
    
    private void Respawn()
    {
        _player.transform.position = _respawnPlayerPosition;
        _player.transform.rotation = _respawnPlayerRotation;
        _player.Camera.transform.rotation = _respawnCameraRotation;
        
        _player.ResetHealth();
    }
    
    private void OnChecked()
    {
        _respawnPlayerPosition = _player.transform.position;
        _respawnPlayerRotation = _player.transform.rotation;
        _respawnCameraRotation = _player.Camera.transform.rotation;
    }
}