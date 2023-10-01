using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DecayingMarine
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MeleeAttacker : MonoBehaviour
    {
        [SerializeField] private float _speed = 3.5f;
        [SerializeField] private float _stunTimeAfterHit = 0.2f;
        [SerializeField] private float _chaseDistance = 25f;
        
        [Header("Enrage charge")]
        [SerializeField] private bool _inCharge;
        [SerializeField] private float _chargeAttackDamage = 10f;
        [SerializeField] private float _chargeAttackForce = 10f;
        [SerializeField] private int _maxHitToEnrage = 5;
        [SerializeField] private int _currentRageChargeCounter;
        [SerializeField] private float _timeBetweenHitsToRage = 0.2f;
        [SerializeField] private float _currentTimeBetweenHitsToRage;
        [SerializeField] private float _chargeTime = 0.5f;
        [SerializeField] private float _chargeSpeed = 10f;
        [SerializeField] private float _chargePreparationTime = 0.5f;
        [SerializeField] private GameObject _chargeDamageZone;

        [Header("Simple attack")]
        [SerializeField] private bool _isSimpleAttack;
        [SerializeField] private float _simpleAttackDamage = 5f;
        [SerializeField] private float _simpleAttackForce = 5f;
        [SerializeField] private float _delayBeforeSimpleAttack = 1f;
        [SerializeField] private GameObject _simpleAttackDamageZone;

        private NavMeshAgent _agent;
        private Enemy _self;

        private static Actor _player;

        private void Start()
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>().GetComponent<Actor>();
            }
            _agent = GetComponent<NavMeshAgent>();
            _self = GetComponent<Enemy>();

            _agent.speed = _speed;
            _currentRageChargeCounter = 0;
            _currentTimeBetweenHitsToRage = 0f;
            _chargeDamageZone.SetActive(false);
        }

        private void Update()
        {
            ChasePlayer();

            if (_currentTimeBetweenHitsToRage > 0f)
            {
                _currentTimeBetweenHitsToRage -= Time.deltaTime;
            }
        }

        private void ChasePlayer()
        {
            if (Vector3.Distance(transform.position, _player.transform.position) > _chaseDistance) return;
            _agent.SetDestination(_player.transform.position);
            transform.LookAt(_player.transform);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }

        private IEnumerator ChargeIntoPlayer()
        {
            _inCharge = true;
            _chargeDamageZone.SetActive(true);
            _self.SetInvulnerable(true);
            _agent.isStopped = true;
            yield return new WaitForSeconds(_chargePreparationTime);
            _agent.isStopped = false;
            _agent.speed = _chargeSpeed;
            yield return new WaitForSeconds(_chargeTime);
            _agent.speed = _speed;
            _self.SetInvulnerable(false);
            _chargeDamageZone.SetActive(false);
            _inCharge = false;
        }

        private IEnumerator Stun()
        {
            _agent.isStopped = true;
            yield return new WaitForSeconds(_stunTimeAfterHit);
            _agent.isStopped = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) return;

            if (_inCharge)
            {
                var impact = new DamageImpact(_chargeAttackDamage, _chargeAttackForce, transform);
                _player.GetHit(impact);
            }
            else
            {
                StartCoroutine(SimpleAttack());
            }
        }

        private IEnumerator SimpleAttack()
        {
            yield return new WaitForSeconds(_delayBeforeSimpleAttack);
            Collider[] targets = Physics.OverlapSphere(_simpleAttackDamageZone.transform.position, 1f);
            for(int i = 0; i < targets.Length; i++)
            {
                if(targets[i].GetComponent<Player>() != null)
                {
                    var impact = new DamageImpact(_simpleAttackDamage, _simpleAttackForce, transform);
                    _player.GetHit(impact);
                }
            }
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        public void OnHit()
        {
            if (_currentTimeBetweenHitsToRage > 0) return;
            StartCoroutine(Stun());
            _currentTimeBetweenHitsToRage = _timeBetweenHitsToRage;
            _currentRageChargeCounter++;

            if(_currentRageChargeCounter >= _maxHitToEnrage)
            {
                StartCoroutine(ChargeIntoPlayer());
                _currentRageChargeCounter = 0;
            }
        }

    }
}
