using Skills;
using UnityEngine;

namespace Doors
{
    public class ExplodingDoor : MonoBehaviour, IShootable
    {
        public void TryApplyShoot(float damage)
        {
            Destroy(gameObject);
        }
    }
}