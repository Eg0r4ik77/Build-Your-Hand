using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IApplyableDamage
{
    [SerializeField] protected float defaultSpeed = 3f;
    [SerializeField] protected float damage = 10f;

    [SerializeField] private Material _hitMaterial;
    [SerializeField] private float _health = 10f;
        
    [SerializeField] private float _pushForce = 2f;
    [SerializeField] private float _pushTime = .5f;

    [SerializeField] private float _flashTime = .5f;

    [SerializeReference] private Player _target;
    
    private MeshRenderer _meshRenderer;
    private bool _isFlashing;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void TryApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
        
        if (!_isFlashing)
        {
            _isFlashing = true;
            StartCoroutine(ApplyFlash());
        }


        Transform victimTransform = transform;
        Vector3 pushVector = (victimTransform.position - ((MonoBehaviour)_target).transform.position).normalized * _pushForce;
        ApplyPush(pushVector, _pushTime);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    public void ApplyPush(Vector3 pushVector, float time)
    {
        StartCoroutine(PushCoroutine(transform, pushVector, time));
    }
        
    private IEnumerator PushCoroutine(Transform victimTransform, Vector3 pushVector, float pushTime)
    {
        //_navMeshAgent.enabled = false;
        StartCoroutine(VictimPusher.Push(victimTransform, pushVector, _pushTime));
        yield return new WaitForSeconds(_pushTime);
        //_navMeshAgent.enabled = true;
    }
        
    private IEnumerator ApplyFlash()
    {
        StartCoroutine(VictimFlasher.Flash(new List<Renderer>(){_meshRenderer}, _hitMaterial, _flashTime));
        yield return new WaitForSeconds(_flashTime);
        _isFlashing = false;
    }
}