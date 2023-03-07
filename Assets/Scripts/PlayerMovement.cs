using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed = 3f;
    [SerializeField, Range(1, 10)] private float _horizontalLookSpeed = 2f;
    
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 motion)
    {
        _characterController.Move(motion * _speed);
    }

    public void RotateHorizontally(float horizontalAxisRotation)
    {
        transform.rotation *= Quaternion.Euler(0, horizontalAxisRotation * _horizontalLookSpeed, 0);
    }
}