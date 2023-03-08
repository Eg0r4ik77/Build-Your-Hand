using System.Collections;
using System.Collections.Generic;
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
        
        [SerializeField] private float _pushForce = 2f;
        [SerializeField] private float _pushTime = .5f;

        [SerializeField] private float _highlightTime = .5f;

        [SerializeReference] private Player _target;
    
        private MeshRenderer _meshRenderer;
        private NavMeshAgent _navMeshAgent;

        private bool _isHighlighted;
        private bool _detectedTarget;
        
        public bool DetectedTarget { get; set; }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _navMeshAgent.speed = _speed;
        }

        public void Attack()
        {
            if (_target)
            {
                _target.TryApplyDamage(_damage);
            }
        }
    
        public void TryApplyDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Die();
            }
        
            if (!_isHighlighted)
            {
                _isHighlighted = true;
                StartCoroutine(ApplyHighlight());
            }
            
            Transform victimTransform = transform;
            Vector3 pushVector = (victimTransform.position - _target.transform.position).normalized * _pushForce;
            ApplyPush(pushVector, _pushTime);
        }

        private void Die()
        {
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
    }
}