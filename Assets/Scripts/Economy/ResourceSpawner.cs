using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Economy
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private List<Resource> _resourcePrefabs;

        private List<Chest> _chests;
        
        private void Awake()
        {
            _chests = FindObjectsOfType<Chest>().ToList();
        }

        private void Start()
        {
            foreach (Chest chest in _chests)
            {
                chest.SetSpawner(this);
            }
        }

        public List<Resource> RandomSpawnResources(Vector3 centrePosition, int resourcesCount)
        {
            var resources = new List<Resource>();
            
            Vector3 chestPosition = centrePosition;
            float verticalOffset = .75f;

            for (int i = 0; i < resourcesCount; i++)
            {
                Vector3 randomPosition = Random.insideUnitSphere;
                Vector3 modifiedRandomPosition = new Vector3(randomPosition.x, chestPosition.y + verticalOffset,
                    randomPosition.x);
                Vector3 spawnPosition = chestPosition + modifiedRandomPosition;

                int randomPrefabIndex = Random.Range(0, _resourcePrefabs.Count);
                resources.Add(Instantiate(_resourcePrefabs[randomPrefabIndex], spawnPosition, Quaternion.identity));
            }

            return resources;
        }
    }
}