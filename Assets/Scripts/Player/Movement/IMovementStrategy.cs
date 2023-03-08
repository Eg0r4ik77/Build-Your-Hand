using UnityEngine;

public interface IMovementStrategy
{
    public Vector3 GetMotion(Vector3 originMotion, float speed);
}