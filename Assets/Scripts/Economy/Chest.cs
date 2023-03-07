using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class Chest : MonoBehaviour, IApplyableDamage
    {
        [SerializeField] private List<int> _purchaseValues;

        private ResourceSpawner _spawner;

        public void SetSpawner(ResourceSpawner spawner)
        {
            _spawner = spawner;
        }
        
        public void TryApplyDamage(float damage)
        {
            StartCoroutine(DestroyCoroutine());
        }
        
        private IEnumerator DestroyCoroutine()
        {
            yield return SpawnResources();
            Destroy(gameObject);
        }
        
        private IEnumerator SpawnResources()
        {
            Vector3 chestPosition = transform.position;
            List<Resource> resources = _spawner.RandomSpawnResources(chestPosition, _purchaseValues.Count);

            for (int i = 0; i < resources.Count; i++)
            {
                resources[i].Value = _purchaseValues[i];
            }
            
            yield return null;
        }
    }
}