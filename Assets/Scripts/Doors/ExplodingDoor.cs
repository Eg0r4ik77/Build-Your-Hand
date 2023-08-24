using Skills;
using UnityEngine;

namespace Doors
{
    public class ExplodingDoor : MonoBehaviour, IShootable
    {
        public void TryApplyShoot(Player player, float damage)
        {
            Destroy(gameObject);
        }
    }
}