using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class EnemyAliveChecker : MonoBehaviour
    {
        //Script gets this objects from interface
        [SerializeField] private DoorLocker _doorLocker;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private int _countOfAliveEnemies;
        [SerializeField] private Transform _enemiesParent;

        private void Start()
        {
            _enemySpawner.OnEnemiesSpawned.AddListener(OnEnemiesSpawned);
        }

        private void OnEnemiesSpawned(List<Enemy> listOfEnemies)
        {
            _countOfAliveEnemies = listOfEnemies.Count;
            foreach (var enemy in listOfEnemies)
            {
                enemy.OnEnemyDie.AddListener(OnEnemyDead);
            }
            StartCoroutine(AutoCheckAfterTime());
        }

        private void OnEnemyDead()
        {
            _countOfAliveEnemies--;
            if (_countOfAliveEnemies <= 0)
            {
                _doorLocker.UnlockDoors();
            }
        }

        private IEnumerator AutoCheckAfterTime()
        {
            while (true)
            {
                if (_enemiesParent.childCount <= 0)
                {
                    Debug.LogWarning("Deleting EnemyAliveChecker Auto");
                    _doorLocker.UnlockDoors();
                    Destroy(gameObject);
                }
                yield return new WaitForSeconds(3f);
            }
        }
    }
}
