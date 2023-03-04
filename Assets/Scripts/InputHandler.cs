using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 3f;

    [SerializeField, Range(1, 10)] private float _lookSpeedX = 2f;
    [SerializeField, Range(1, 10)] private float _lookSpeedY = 2f;
    
    [SerializeField, Range(1, 180)] private float _upperLookLimit = 80f;
    [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80f;

    [SerializeField] private Camera _camera;
    
    private PlayerMovement _playerMovement;

    private Vector2 _currentInput;

    private float _rotationX;

    private float VerticalAxis => Input.GetAxis("Vertical");
    private float HorizontalAxis => Input.GetAxis("Horizontal");
    
    private float MouseHorizontalAxis => Input.GetAxis("Mouse X");
    private float MouseVerticalAxis => Input.GetAxis("Mouse Y");
    
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
    }

    private void HandleMovementInput()
    {
        Vector3 motion = transform.forward * VerticalAxis + transform.right * HorizontalAxis;
        _playerMovement.Move(motion * (_walkSpeed * Time.deltaTime));
    }

    private void HandleMouseLook()
    {
        _rotationX -= MouseVerticalAxis * _lookSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_upperLookLimit, _lowerLookLimit);
        
        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, MouseHorizontalAxis * _lookSpeedX, 0);
    }
}