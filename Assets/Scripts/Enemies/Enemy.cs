using System;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.BehaviourTrees;
using Skills;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent), typeof(MeshRenderer))]
    public class Enemy : MonoBehaviour, IApplyableDamage, IShootable
    {
        [SerializeField] protected float _speed = 2f;
        [SerializeField] protected float _damage = 10f;
        
        [SerializeField] private float _health = 10f;
        [SerializeField] private Material _hitMaterial;
        [SerializeField] private Transform _spawnPoint;
        
        [SerializeField] private float _pushForce = 2f;
        [SerializeField] private float _pushTime = .5f;

        [SerializeField] private float _highlightTime = .5f;

        private MeshRenderer _meshRenderer;
        private NavMeshAgent _navMeshAgent;
        private BehaviourTreeOwner _behaviourTreeOwner;
        
        private bool _isHighlighted;
        private bool _detectedTarget;

        public IEnemyTarget Target { get; set; }

        public bool DetectedTarget => _detectedTarget;

        public Transform SpawnPoint => _spawnPoint;

        public event Action<Enemy> Detected;
        public event Action Died;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _behaviourTreeOwner = GetComponent<BehaviourTreeOwner>();
        }

        private void Start()
        {
            _navMeshAgent.speed = _speed;
        }
        
        public void Attack()
        {
            Target?.TryApplyDamage(_damage);
        }

        public void TryApplyDamage(float damage)
        {
            if (IsDead())
            {
                return;
            }
            
            _health -= damage;
            TryDetectTarget();

            if (IsDead())
            {
                Die();
            }
        
            if (!_isHighlighted)
            {
                _isHighlighted = true;
                StartCoroutine(ApplyHighlight());
            }
            
            Transform victimTransform = transform;
            Vector3 normalizedVector =
                (victimTransform.position - ((MonoBehaviour)Target).transform.position).normalized;
            Vector3 pushVector = normalizedVector * _pushForce;
            
            ApplyPush(pushVector, _pushTime);
        }

        public bool IsDead()
        {
            return _health <= 0;
        }
        
        private void Die()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }

        private void ApplyPush(Vector3 pushVector, float time)
        {
            StartCoroutine(PushCoroutine(transform, pushVector, time));
        }
        
        private IEnumerator PushCoroutine(Transform victimTransform, Vector3 pushVector, float pushTime)
        {
            _navMeshAgent.enabled = false;
            StartCoroutine(VictimPusher.Push(victimTransform, pushVector, _pushTime));
            yield return new WaitForSeconds(_pushTime);
            _navMeshAgent.enabled = true;
        }
        
        private IEnumerator ApplyHighlight()
        {
            StartCoroutine(VictimHighlighter.Highlight(new List<Renderer>(){_meshRenderer}, _hitMaterial, _highlightTime));
            yield return new WaitForSeconds(_highlightTime);
            _isHighlighted = false;
        }
        
        public void TryApplyShoot(float damage)
        {
            TryApplyDamage(damage);
        }

        public void TryDetectTarget()
        {
            if (!_detectedTarget)
            {
                _detectedTarget = true;
                Detected?.Invoke(this);
            }
        }
    }
}