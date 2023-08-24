using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Data/Attack/BombEnemy")]
public class BombEnemyAttackConfig : ScriptableObject
{
    [field:SerializeField] public float Range { get; private set; }
    [field:SerializeField] public float DelayBeforeExplosion { get; private set; }
    [field:SerializeField] public BombExplosion Explosion{ get; private set; }
}