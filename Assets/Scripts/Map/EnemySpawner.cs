using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DecayingMarine
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private bool _isActive;
        [SerializeField] private int _minCountOfEnemies = 2;
        [SerializeField] private int _maxCountOfEnemies = 4;

        [SerializeField] private MeshFilter _floorMeshFilter;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _enemiesParent;

        private List<Enemy> _listOfSpawnedEnemies;

        public UnityEvent<List<Enemy>> OnEnemiesSpawned;

        private void Awake()
        {
            _isActive = true;
            _listOfSpawnedEnemies = new List<Enemy>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) return;

            SpawnEnemies();
            OnEnemiesSpawned?.Invoke(_listOfSpawnedEnemies);
        }

        public void SpawnEnemies()
        {
            if (!_isActive) return;

            int enemiesCount = Random.Range(_minCountOfEnemies, _maxCountOfEnemies);
            for(int i = 0; i < enemiesCount; i++)
            {
                SpawnEnemy();
            }

            _isActive = false;
        }

        private void SpawnEnemy()
        {
            Vector3 enemyPos = Random.insideUnitSphere * 15f + gameObject.transform.position;

            var newEnemy = Instantiate(_enemyPrefab, new Vector3(enemyPos.x, transform.position.y, enemyPos.z), Quaternion.identity, _enemiesParent.transform).GetComponent<Enemy>();
            if (newEnemy != null)
            {
                _listOfSpawnedEnemies.Add(newEnemy);
            }
            else
            {
                Debug.Log($"{newEnemy.gameObject.name} without enemy script!");
            }
        }
    }
}
