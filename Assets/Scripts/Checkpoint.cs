using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Action Checked;
    
    private bool _checked;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && !_checked)
        {
            Check(player);
        }
    }

    private void Check(Player player)
    {
        player.ResetHealth();
        
        _checked = true;
        Checked?.Invoke();
    }
}
