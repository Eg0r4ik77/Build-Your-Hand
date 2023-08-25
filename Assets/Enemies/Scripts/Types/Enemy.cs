using System;
using Enemies.Pool;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using Skills;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IPoolObject, IApplyableDamage, IShootable, IPauseable
    {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private Transform _center;

        protected Animator animator;
        protected NavMeshAgent navMeshAgent;
        protected BehaviourTreeOwner behaviourTreeOwner;

        protected Patrol patrol;
        protected Reaction reaction;
        
        private CapsuleCollider _collider;
        
        private float _health;
        private float _viewAngle;
        private float _detectDistance;
        
        private float _maxHealth;

        private EnemyTargetDetector _targetDetector;

        public Transform Center => _center;
        public IEnemyTarget Target { get; set; }
        public bool CanApplyDamage { get; set; }

        public bool InUse { get; set; }
        
        public Action<Enemy, IEnemyTarget> DamagedByEnemyTarget;
        public Action<Enemy> ReturnedToPool;
        
        protected void Awake()
        {
            CacheEnemyComponents();
        }

        public void Initialize()
        {
            SetConfigValues();
            InitializeEnemyComponents();
            InitializeBehaviourTreeVariables();
            
            _maxHealth = _health;
            
            TryFlipScale();
        }
        
        public void Clear()
        {
            _health = _maxHealth;
            CanApplyDamage = true;
            Target = null;
            
            behaviourTreeOwner.enabled = true;
            gameObject.SetActive(false);
        }

        public void TryApplyShoot(Player player, float damage)
        {
            TryApplyDamage(player, damage);
        }

        public void TryApplyDamage(IEnemyTarget target, float damage)
        {
            DamagedByEnemyTarget?.Invoke(this, target);
            TryApplyDamage(damage);
        }
        
        public virtual void TryApplyDamage(float damage)
        {
            if (!CanApplyDamage || damage <= 0)
                return;
            
            _health -= damage;
            
            if (_health <= 0)
            {
                Die();
                return;
            }
            
            if (reaction)
            {
                reaction.React();
            }
        }
        
        public void SetPaused(bool paused)
        {
            behaviourTreeOwner.enabled = !paused;
            navMeshAgent.enabled = !paused;
            animator.enabled = !paused;
        }

        public void ReturnToPool()
        {
           ReturnedToPool?.Invoke(this); 
        }
        
        public bool TryDetect(IEnemyTarget target)
        {
            float distanceToCurrentTarget = GetDistanceToTarget(target);
            return Target == null && distanceToCurrentTarget <= _detectDistance;
        }

        public bool IsCurrentTargetInViewAngle()
        {
            Vector3 enemyForwardVector = transform.forward;
            Vector3 vectorFromEnemyToVictim = Target.CenterPosition - transform.position;
            
            enemyForwardVector.y = vectorFromEnemyToVictim.y = 0;
       
            float angleBetweenEnemyAndTarget = Vector3.Angle(enemyForwardVector, vectorFromEnemyToVictim);
            
            return angleBetweenEnemyAndTarget < _viewAngle;
        }
        
        
        public float GetDistanceToCurrentTarget()
        {
            float distance = GetDistanceToTarget(Target);
            return distance;
        }

        protected virtual void CacheEnemyComponents()
        {
            _collider = GetComponent<CapsuleCollider>();
            
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            behaviourTreeOwner = GetComponent<BehaviourTreeOwner>();
        }

        protected abstract void InitializeEnemyComponents();

        protected abstract void InitializeBehaviourTreeVariables();

        protected void SetBehaviourTreeVariable(string variableName, object value)
        {
            behaviourTreeOwner.graph.blackboard.SetVariableValue(variableName, value);
        }
        
        protected virtual void Die()
        {
            ReturnToPool();
        }

        private void SetConfigValues()
        {
            _health = _config.Health;
            _viewAngle = _config.ViewAngle;
            _detectDistance = _config.DetectDistance;
        }

        private void TryFlipScale()
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Random.Range(0, 2) * 2 - 1;
            transform.localScale = newScale;
        }
        
        private float GetDistanceToTarget(IEnemyTarget target)
        {
            float distance; 
                
            CapsuleCollider targetCollider = target.Collider;

            Vector3 enemyCenterPosition = _center.position;
            Vector3 targetCenterPosition = target.CenterPosition;

            float enemyCenterHeight = _collider.height / 2;
            float targetCenterHeight = targetCollider.height / 2;
            
            float centersHeightsDifference = enemyCenterHeight - targetCenterHeight;
            float centersHeightHalfSum = (enemyCenterHeight + targetCenterHeight) / 2;

            float enemyOffset = enemyCenterHeight - _collider.radius;
            float targetOffset = targetCenterHeight - targetCollider.radius;
            
            if (Mathf.Abs(centersHeightsDifference) < centersHeightHalfSum)
            {
                distance = Vector3.Distance(enemyCenterPosition, targetCenterPosition);
            }
            else if (centersHeightsDifference < 0)
            {
                Vector3 topEnemyCenter = enemyCenterPosition + Vector3.up * enemyOffset;
                Vector3 bottomTargetCenter = targetCenterPosition + Vector3.down * targetOffset;
                
                distance = Vector3.Distance(topEnemyCenter, bottomTargetCenter);
            }
            else
            {
                Vector3 bottomEnemyCenter = enemyCenterPosition + Vector3.down * enemyOffset;
                Vector3 topTargetCenter = targetCenterPosition + Vector3.up * targetOffset;
                
                distance = Vector3.Distance(bottomEnemyCenter, topTargetCenter);
            }

            distance -= _collider.radius + targetCollider.radius;

            return distance;
        }
    }
}