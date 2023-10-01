using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class PunchDamageZone : MonoBehaviour
    {
        [SerializeField] private float _damageMin;
        [SerializeField] private float _damageMax;
        [SerializeField] private float _force;
        [SerializeField] private AudioClip _hitSound;
        [SerializeField] private AudioClip _missSound;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponentInParent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            if(enemy != null)
            {
                var impact = new DamageImpact(Random.Range(_damageMin, _damageMax), _force, transform);
                enemy.GetHit(impact);
                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.PlayOneShot(_hitSound);
            }
            else
            {
                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.PlayOneShot(_missSound);
            }
        }
    }
}
