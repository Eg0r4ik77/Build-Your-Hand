using Enemies;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public bool Finished { get; set; } = true;

    protected abstract void SetConfigValues();
    
    protected static bool CheckAttackConditions(Enemy enemy, float range)
    {
        float distanceToCurrentTarget = enemy.GetDistanceToCurrentTarget();
        bool isCurrentTargetInDetectDistance = distanceToCurrentTarget <= range;
    
        return isCurrentTargetInDetectDistance && enemy.IsCurrentTargetInViewAngle();
    }
}
