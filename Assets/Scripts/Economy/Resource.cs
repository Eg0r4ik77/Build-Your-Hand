using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Economy
{
    public class Resource : MonoBehaviour
    {
        private Quaternion _rotation;
        public float Value { get; set; }

        private void Start()
        {
            _rotation = transform.rotation;
            SetRandomRotation();
        }

        private void Update()
        {
            transform.rotation *= _rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.AddResource(this);
                Destroy(gameObject);
            }
        }

        private void SetRandomRotation()
        {
            Random random = new Random();
            float angle = .5f;
            
            List<Vector3> rotationDirections = new List<Vector3>()
            {
                Vector3.up,
                Vector3.forward,
                Vector3.right
            };

            rotationDirections = rotationDirections.OrderBy(_ => random.Next()).ToList();

            _rotation *= Quaternion.AngleAxis(angle, rotationDirections[0]) *
                         Quaternion.AngleAxis(angle, rotationDirections[1]);
        }
    }
}