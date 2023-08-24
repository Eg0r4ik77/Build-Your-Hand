using Enemies;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private MovementConfig _config;

    private const float MaxDistanceAroundNavmeshPoint = 100f;
    private static readonly int WalkingHash = Animator.StringToHash("Walking");

    private Enemy _enemy;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private float _movementSpeed;
    private float _rotationSpeed;
    private float _stoppingDistance;

    public float Speed 
    {
        set => _navMeshAgent.speed = _navMeshAgent.acceleration = value;
    }
    
    public bool IsMoving
    {
        get => !_navMeshAgent.isStopped;
        set => _navMeshAgent.isStopped = !value;
    }


    private void Start()
    {
        SetConfigValues();
        Initialize();
    }

    private void Update()
    {
        if (_enemy != null && _enemy.Target != null)
        {
            RotateToTarget();
        }
    }

    public void Initialize(Enemy enemy, NavMeshAgent navMeshAgent, Animator animator)
    {
        _enemy = enemy;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public static Vector3? SamplePosition(Vector3 sourcePosition)
    {
        if (NavMesh.SamplePosition(sourcePosition, out var hit, MaxDistanceAroundNavmeshPoint, NavMesh.AllAreas))
        {
            Vector3 sampledPosition = hit.position;
            return sampledPosition;
        }
        
        return null;
    }
    
    public void SetTargetAsDestination()
    {
        IEnemyTarget target = _enemy.Target;
        if (target != null)
        {
            Vector3 targetPosition = target.CenterPosition;
            SetDestination(targetPosition);
        }
    }
    
    public void SetDestination(Vector3 position)
    {
        var targetPosition = SamplePosition(position);

        if (targetPosition == null || ReachedPosition(targetPosition.Value))
        {
            Stop();
            return;
        }        
        
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(targetPosition.Value);

        _animator.SetBool(WalkingHash, true);
    }

    public bool ReachedDestination()
    {
        bool reachedDestination = ReachedPosition(_navMeshAgent.destination);
        return reachedDestination;
    }

    public void Stop()
    {
        _navMeshAgent.SetDestination(transform.position);
        _navMeshAgent.isStopped = true;
        
        _animator.SetBool(WalkingHash, false);
    }
    
    private void Initialize()
    {
        Speed = _movementSpeed;
        _navMeshAgent.angularSpeed = _rotationSpeed;
    }
    
    private void SetConfigValues()
    {
        _movementSpeed = _config.MovementSpeed;
        _rotationSpeed = _config.RotationSpeed;
        _stoppingDistance = _config.StoppingDistance;
    }

    private void RotateToTarget()
    {
        Vector3 targetPosition = _enemy.Target.CenterPosition;
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;
        
        Quaternion rotation = Quaternion.LookRotation(direction);
        float degreesDelta = _rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, degreesDelta);
    }
    
    private bool ReachedPosition(Vector3 position)
    {
        float remainingDistance = Vector3.Distance(transform.position, position);
        return remainingDistance <= _stoppingDistance;
    }
}
