using UnityEngine;

namespace Movement
{
    public class SimpleMovement : IMovementStrategy
    {
        public Vector3 GetMotion(Vector3 originMotion, float speed)
        {
            return originMotion * speed;
        }
    }
}
