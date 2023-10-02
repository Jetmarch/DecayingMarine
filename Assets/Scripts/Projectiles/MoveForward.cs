using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DecayingMarine
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _maxTimeToLive = 10f;
        [SerializeField] private float _minDamage = 3f;
        [SerializeField] private float _maxDamage = 5f;
        private static Actor _player;

        private void Start()
        {
            _player = FindObjectOfType<Player>().GetComponent<Actor>();
            Destroy(gameObject, _maxTimeToLive);
        }

        private void Update()
        {
            transform.Translate(transform.forward * 5f * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() != null)
            {
                var impact = new DamageImpact(Random.Range(_minDamage, _maxDamage), 0f, transform);
                _player.GetHit(impact);
                Destroy(gameObject);
            }

            
        }
    }
}
