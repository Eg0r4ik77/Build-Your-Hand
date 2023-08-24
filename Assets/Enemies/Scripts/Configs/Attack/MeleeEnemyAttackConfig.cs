using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Data/Attack/MeleeEnemy")]
public class MeleeEnemyAttackConfig : ScriptableObject
{
    [field:SerializeField] public float Damage { get; private set; }
    [field:SerializeField] public float Range { get; private set; }
    [field:SerializeField] public float Cooldown { get; private set; }
    [field:SerializeField] public float PunchProbability { get; private set; }
}