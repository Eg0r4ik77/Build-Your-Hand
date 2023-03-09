using UnityEngine;
using UnityEngine.UI;

public class CursorSwitcher : MonoBehaviour
{ 
    [SerializeField] private Image _predictionPointImage;

    public void SwitchCursorToPredictionPoint()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _predictionPointImage.gameObject.SetActive(true);
    }
    
    public void SwitchPredictionPointToCursor()
    {
        _predictionPointImage.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}