using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        Enable();
    }

    protected void OnDisable()
    {
        Disable();
    }

    protected virtual void Enable()
    {
        _closeButton.onClick.AddListener(Close);
    }

    protected virtual void Disable()
    {
        _closeButton.onClick.RemoveListener(Close);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }
}