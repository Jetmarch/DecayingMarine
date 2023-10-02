using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;

namespace DecayingMarine
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        
        [SerializeField] private float _invulnerableTimeAfterHit = 0.1f;
        [SerializeField] private bool _isInvulnerable;
        [SerializeField] private float _repulsionAmount = 1f;

        private Health _health;
        private AudioSource _audioSource;
        private Rigidbody _rigidbody;
        private AIAgent _aiAgent;

        [Header("Sounds")]
        [SerializeField] private AudioClip _getHitSound;

        public UnityEvent OnEnemyDie;

        private void Start()
        {
            _health = GetComponent<Health>();
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();
            _aiAgent = GetComponent<AIAgent>();

            _health.OnDie.AddListener(OnDie);
        }

        private void OnDie()
        {
            _aiAgent.enabled = false;
            OnEnemyDie?.Invoke();
            transform.DOScale(0f, 1f);
            Destroy(gameObject, 1.5f);
        }

        public void GetHit(DamageImpact impact)
        {
            if (_isInvulnerable) return;
            if (_health.IsDead) return;

            Vector3 repulsion = ((transform.position - impact.Attacker.position).normalized * impact.Force) * _repulsionAmount;
            if (repulsion != Vector3.zero)
            {
                _rigidbody.AddForce(repulsion, ForceMode.Impulse);
            }
            _health.DoDamage(impact);
            _aiAgent.OnHit();
            _audioSource.PlayOneShot(_getHitSound);
        }

        public void SetInvulnerable(bool state)
        {
            _isInvulnerable = state;
        }
    }
}
