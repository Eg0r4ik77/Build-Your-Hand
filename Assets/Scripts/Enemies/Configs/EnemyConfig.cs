using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Data/Base")]
public class EnemyConfig : ScriptableObject
{
    [field:SerializeField] public float Health { get; private set; }
    [field:SerializeField] public float DetectDistance { get; private set; }
    [field:SerializeField] public float ViewAngle { get; private set; }
}
