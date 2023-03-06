using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private CameraShaker _shaker;
    
    [SerializeField, Range(1, 10)] private float _verticalLookSpeed = 2f;
    
    [SerializeField, Range(1, 180)] private float _upperLookLimit = 80f;
    [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80f;

    private Camera _camera;
    private float _horizontalRotation;
    private float _verticalRotation;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        _shaker.SetCamera(_camera.transform);
    }

    public void RotateVertically(float verticalAxisRotation)
    {
        _horizontalRotation -= verticalAxisRotation * _verticalLookSpeed;
        _horizontalRotation = Mathf.Clamp(_horizontalRotation, -_upperLookLimit, _lowerLookLimit);
        
        _camera.transform.localRotation = Quaternion.Euler(_horizontalRotation, 0, 0);
    }

    public void Shake()
    {
        _shaker.Shake();
    }
}