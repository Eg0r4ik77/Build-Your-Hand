using UnityEngine;

namespace Movement
{
    public class AcceleratedMovement : IMovementStrategy
    {
        private float Acceleration { get;}
        
        public AcceleratedMovement(float acceleration)
        {
            Acceleration = acceleration;
        }

        public Vector3 GetMotion(Vector3 originMotion, float speed)
        {
            return originMotion * (speed * Acceleration);
        }
    }
}