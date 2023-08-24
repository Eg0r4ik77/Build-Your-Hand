using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;
    
    private List<IEnemyTarget> _targets;
    private int _layerMask;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(List<IEnemyTarget> targets, int layerMask)
    {
        _targets = targets;
        _layerMask = layerMask;
    }

    public void Execute()
    {
        _particleSystem.Play();
        _audioSource.Play();

        DealDamageToAffectedAreaTargets();
        Destroy(gameObject, _audioSource.clip.length);
    }

    private void DealDamageToAffectedAreaTargets()
    {
        foreach (IEnemyTarget target in _targets)
        {
            Vector3 direction = (target.CenterPosition - transform.position).normalized;

            if (Physics.Raycast(transform.position, direction, _radius, _layerMask))
            {
                target.TryApplyDamage(_damage);
            }
        }
    }
}