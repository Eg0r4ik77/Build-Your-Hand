using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    private CharacterController _characterController;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

    }

    public void Move(Vector3 motion)
    {
        _characterController.Move(motion);
    }
}