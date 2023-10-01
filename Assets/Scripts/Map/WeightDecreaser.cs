using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class WeightDecreaser : MonoBehaviour
    {
        [SerializeField] private float _minWeightDecrease = 5f;
        [SerializeField] private float _maxWeightDecrease = 10f;
        [SerializeField] private float _delayBeforeWeightDecrease = 2f;
        [SerializeField] private bool _isActive;

        private static Inventory _inventory;

        private void Start()
        {
            if (_inventory == null)
            {
                _inventory = FindObjectOfType<Player>().GetComponent<Inventory>();
            }
            _isActive = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isActive) return;

            StartCoroutine(DelayedDecrease());
            _isActive = false;
        }

        private IEnumerator DelayedDecrease()
        {
            yield return new WaitForSeconds(_delayBeforeWeightDecrease);
            float weightDecreate = Random.Range(_minWeightDecrease, _maxWeightDecrease);
            _inventory.DecreaseMaxWeight(weightDecreate);
        }
    }
}
