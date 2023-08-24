using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Data/Patrol")]
public class PatrolConfig : ScriptableObject
{
    [field: SerializeField] public float MaxDistance { get; private set; }
    [field: SerializeField] public Vector2 WaitingTimeSecondsRange { get; private set; }
}
