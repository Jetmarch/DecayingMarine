using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    
    public class RaycastGunItem : Item
    {
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;
        [SerializeField] private float _spreadValue = 0.04f;
        [SerializeField] private float _timeBetweenShots = 0.4f;
        private float _currentTimeBetweenShots;
        [SerializeField] private int _countOfShotsInOneUse = 1;
        [SerializeField] private float _force;
        [SerializeField] private bool _isReadyToShoot;

        [Header("Sounds")]
        [SerializeField] private AudioClip _shootSound;


        private AudioSource _audio;
        private BulletSpawnPoint _bulletSpawnPoint;
        private RaycastHit _hit;

        private void Start()
        {
            _bulletSpawnPoint = FindObjectOfType<BulletSpawnPoint>();
            _audio = GetComponent<AudioSource>();
            _isReadyToShoot = true;
        }

        private void Update()
        {
            if(_currentTimeBetweenShots < _timeBetweenShots)
            {
                _currentTimeBetweenShots += Time.deltaTime;
            }

            if(_currentTimeBetweenShots >= _timeBetweenShots && !_isReadyToShoot)
            {
                _isReadyToShoot = true;
            }
        }

        private void Shoot()
        {
            Vector3 rayDirection = transform.TransformDirection(Vector3.forward) + new Vector3(Random.Range(-_spreadValue, _spreadValue), 0f, Random.Range(-_spreadValue, _spreadValue));
            if (Physics.Raycast(transform.position, rayDirection, out _hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, rayDirection * _hit.distance, Color.yellow, .3f);
                DoImpactOnTarget();
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white, .3f);
            }
        }

        private void DoImpactOnTarget()
        {
            var actor = _hit.transform.gameObject.GetComponent<Actor>();
            if(actor != null)
            {
                var impact = new DamageImpact(Random.Range(_minDamage, _maxDamage), _force, transform);
                actor.GetHit(impact);
            }
            else
            {

            }
        }

        public override void Use()
        {
            if (!_isAvailable) return;
            if (!_isReadyToShoot) return;
            
            for(int i = 0; i < _countOfShotsInOneUse; i++)
            {
                Shoot();
            }
            _audio.PlayOneShot(_shootSound);
            _isReadyToShoot = false;
            _currentTimeBetweenShots = 0f;
        }
    }
}
