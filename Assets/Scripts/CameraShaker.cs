using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private AnimationCurve _shakeCurve;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private float _evaluateMultiplier = 2f;
    
    private Vector3 _offsetVector;
    private Vector3 _originPosition;

    public void SetCamera(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
        _originPosition = _cameraTransform.localPosition;
    }

    private void Update()
    {
        if (_cameraTransform)
        {
            _cameraTransform.localPosition = _originPosition + _offsetVector;
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine(){
        for (float t = 0; t < _duration; t += Time.deltaTime)
        {
            float curveMultiplier = _shakeCurve.Evaluate(t * _evaluateMultiplier);
            _offsetVector = Vector3.up * curveMultiplier;
            
            yield return null;
        }

        _offsetVector = Vector3.zero;
    }
}