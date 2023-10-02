using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        [SerializeField] private GameObject _hitImpactPrefab;
        [SerializeField] private GameObject _muzzlePrefab;
        [SerializeField] private Transform _muzzleSpawnPoint;


        private AudioSource _audio;
        private RaycastHit _hit;
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            _lineRenderer = GetComponent<LineRenderer>();
            _isReadyToShoot = true;
            _isDisposable = false;
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
            if (_lineRenderer != null)
            {
                _lineRenderer.SetPosition(0, _muzzleSpawnPoint.position);
            }
            if (_muzzlePrefab != null)
            {
                Destroy(Instantiate(_muzzlePrefab, _muzzleSpawnPoint.position, _muzzleSpawnPoint.rotation, transform), .1f);
            }

            Vector3 rayDirection = transform.TransformDirection(Vector3.forward) + new Vector3(Random.Range(-_spreadValue, _spreadValue), 0f, Random.Range(-_spreadValue, _spreadValue));
            if (Physics.Raycast(transform.position, rayDirection, out _hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, rayDirection * _hit.distance, Color.yellow, .3f);
                if (_lineRenderer != null)
                {
                    _lineRenderer.SetPosition(1, _hit.point);
                    StartCoroutine(DeleteLineAfterDelay());
                }
                DoImpactOnTarget();
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white, .3f);
            }
        }

        private IEnumerator DeleteLineAfterDelay()
        {
            yield return new WaitForSeconds(0.2f);
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
        }

        private void DoImpactOnTarget()
        {
            var enemy = _hit.transform.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                var impact = new DamageImpact(Random.Range(_minDamage, _maxDamage), _force, transform);
                enemy.GetHit(impact);
            }
            var hitImpact = Instantiate(_hitImpactPrefab, _hit.point + (_hit.normal * 0.25f), Quaternion.identity);
            hitImpact.transform.localScale *= 1.5f;
        }

        public override void Use()
        {
            if (!_isAvailable) return;
            if (!_isReadyToShoot) return;
            
            for(int i = 0; i < _countOfShotsInOneUse; i++)
            {
                Shoot();
            }
            _audio.pitch = Random.Range(0.9f, 1.1f);
            _audio.PlayOneShot(_useSound);
            _isReadyToShoot = false;
            _currentTimeBetweenShots = 0f;
        }
    }
}
