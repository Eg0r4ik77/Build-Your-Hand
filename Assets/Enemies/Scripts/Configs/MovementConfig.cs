using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Data/Movement")]
public class MovementConfig : ScriptableObject
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float StoppingDistance { get; private set; }

}
