using Movement;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField, Range(1, 10)] private float _horizontalLookSpeed = 2f;
    
    private CharacterController _characterController;
    
    public IMovementStrategy Strategy { get; set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        Strategy = new SimpleMovement();
    }

    public void Move(Vector3 motion)
    {
        Vector3 resultMotion = Strategy.GetMotion(motion, _speed);
        
        _characterController.Move(resultMotion);
    }

    public void RotateHorizontally(float horizontalAxisRotation)
    {
        transform.rotation *= Quaternion.Euler(0, horizontalAxisRotation * _horizontalLookSpeed, 0);
    }
}