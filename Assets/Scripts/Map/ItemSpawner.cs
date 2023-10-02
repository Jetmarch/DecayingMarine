using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private bool _isActive;
        [SerializeField] private bool _isSpawnOnStart;

        private void Start()
        {
            _isActive = true;
            if(_isSpawnOnStart)
            {
                SpawnItem();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) return;
            SpawnItem();
        }

        private void SpawnItem()
        {
            if (!_isActive) return;

            var randomItem = RandomItemGenerator.instance.GetRandomItem();
            Instantiate(randomItem, _spawnPoint.position, Quaternion.identity);
            _isActive = false;
        }
    }
}
