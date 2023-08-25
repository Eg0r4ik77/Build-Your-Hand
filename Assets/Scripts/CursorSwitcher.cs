using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CursorSwitcher 
{ 
    [SerializeField] private Image _predictionPointImage;

    public void SwitchToPredictionPoint()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _predictionPointImage.gameObject.SetActive(true);
    }
    
    public void SwitchToCursor()
    {
        _predictionPointImage.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}