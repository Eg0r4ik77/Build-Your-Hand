using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Patrol : MonoBehaviour
{
    [SerializeField] private PatrolConfig _config;
    
    private float _maxDistance;
    private Vector2 _waitingTimeRangeSeconds;
    
    private Vector3 _startPosition;
    private EnemyMovement _movement;

    private void Start()
    {
        SetConfigValues();
    }

    public void Initialize(EnemyMovement movement)
    {
        _movement = movement;
    }

    public void SetStartPosition(Vector3 position)
    {
        _startPosition = position;
    }

    public async Task SetRandomDestinationAsync(CancellationTokenSource cancellationTokenSource)
    {
        float minWaitingTime = _waitingTimeRangeSeconds.x;
        float maxWaitingTime = _waitingTimeRangeSeconds.y;
        
        float delayInSeconds = Random.Range(minWaitingTime,maxWaitingTime);
        int delayInMilliseconds = (int)(delayInSeconds * 1000);

        await Task.Delay(delayInMilliseconds, cancellationTokenSource.Token);
			
        SetRandomDestination();
    }

    public void SetRandomDestination()
    {
        SetRandomDestinationInArea(_startPosition, _maxDistance);
        _movement.IsMoving = true;
    }

    private void SetConfigValues()
    {
        _maxDistance = _config.MaxDistance;
        _waitingTimeRangeSeconds = _config.WaitingTimeSecondsRange;
    }
		
    private void SetRandomDestinationInArea(Vector3 startPosition, float range)
    {   
        Vector3 randomDirection = Random.insideUnitSphere * range;
        Vector3 randomDestination = startPosition + randomDirection;

        Vector3? sampledDestination = EnemyMovement.SamplePosition(randomDestination);
        Vector3 destination = sampledDestination ?? startPosition;

        _movement.SetDestination(destination);
    }
}
