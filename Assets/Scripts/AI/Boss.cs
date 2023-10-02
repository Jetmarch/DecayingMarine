using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Events;

namespace DecayingMarine
{
    public class Boss : AIAgent
    {
        [Header("Jump settings")]
        [SerializeField] private float _jumpAttackMinDamage = 7f;
        [SerializeField] private float _jumpAttackMaxDamage = 12f;
        [SerializeField] private float _jumpAttackForce = 25f;
        [SerializeField] private float _minJumpRadius = 5f;
        [SerializeField] private float _maxJumpRadius = 10f;
        [SerializeField] private int _minCountOfJumps = 1;
        [SerializeField] private int _maxCountOfJumps = 3;
        [SerializeField] private float _jumpDuration = 1f;
        [SerializeField] private float _jumpPower = 5f;
        [SerializeField] private float _delayBeforeJump = 1.5f;
        [SerializeField] private float _chanceOfJumpOnPlayer = 35f;
        [SerializeField] private bool _inJump;
        [SerializeField] private MeshFilter _floorMesh;

        [Header("Projectile circle attack")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private int _minCountOfProjectilesInCircle;
        [SerializeField] private int _maxCountOfProjectilesInCircle;
        private Bounds _floorBounds;

        private Health _health;
        private NavMeshAgent _agent;
        private Actor _player;
        private MeshRenderer _meshRenderer;

        public UnityEvent OnBossDead;

        private void Start()
        {
            _health = GetComponent<Health>();
            _agent = GetComponent<NavMeshAgent>();
            _player = FindObjectOfType<Player>().GetComponent<Actor>();
            _meshRenderer = GetComponent<MeshRenderer>();
            _floorBounds = _floorMesh.mesh.bounds;
            _inJump = false;
        }

        private IEnumerator RandomlyJumpingAroundTheRoom()
        {
            yield return new WaitForSeconds(2f);
            while(!_health.IsDead)
            {
                Vector3 newJumpPoint;
                if (Vector3.Distance(transform.position, _player.transform.position) < 25f)
                {
                    if (Random.Range(0, 100) > _chanceOfJumpOnPlayer)
                    {
                        newJumpPoint = GetRandomJump();
                    }
                    else
                    {
                        newJumpPoint = GetJumpToPlayer();
                    }
                }
                else
                {
                    newJumpPoint = GetJumpToPlayer();
                }

                newJumpPoint.y = -1f;
                transform.DOJump(newJumpPoint, _jumpPower, Random.Range(_minCountOfJumps, _maxCountOfJumps), _jumpDuration).OnComplete(() =>
                {
                    _inJump = false;
                    SpawnProjectileCircle();
                });
                _inJump = true;
                yield return new WaitForSeconds(_delayBeforeJump);
            }
            OnBossDead?.Invoke();
        }

        private Vector3 GetRandomJump()
        {
            return _floorBounds.ClosestPoint((Random.insideUnitSphere * Random.Range(_minJumpRadius, _maxJumpRadius) * 5f)) + transform.position;
        }

        private Vector3 GetJumpToPlayer()
        {
            return _player.transform.position + (Random.insideUnitSphere * Random.Range(_minJumpRadius, _maxJumpRadius));
        }

        private void SpawnProjectileCircle()
        {
            float yRotation = 0f;
            int countOfProjectiles = Random.Range(_minCountOfProjectilesInCircle, _maxCountOfProjectilesInCircle);
            for(int i = 1; i <= countOfProjectiles; i++)
            {
                Instantiate(_projectilePrefab, transform.position, Quaternion.Euler(0f, yRotation, 0f));
                yRotation = (360 / countOfProjectiles) * i;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_inJump) return;
            if (other.GetComponent<Player>() == null) return;

            var impact = new DamageImpact(Random.Range(_jumpAttackMinDamage, _jumpAttackMaxDamage), _jumpAttackForce, transform);
            _player.GetHit(impact);
        }

        public override void OnHit()
        {
            Color prevColor = _meshRenderer.material.color;
            _meshRenderer.material.DOColor(Color.red, 0.1f).OnComplete(() => { _meshRenderer.material.DOColor(prevColor, 0.1f); });
        }

        public void StartAction()
        {
            StartCoroutine(RandomlyJumpingAroundTheRoom());
        }
    }
}
