using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Projectile Data")]
    public class ProjectileConfig : ScriptableObject
    {
        [field:SerializeField] public float Speed { get; private set; }
        [field:SerializeField] public float Acceleration { get; private set; }
        [field:SerializeField] public float LaunchDurationInSeconds { get; private set; }
    }
}