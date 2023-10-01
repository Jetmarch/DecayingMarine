using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DecayingMarine
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        
        [SerializeField] private float _invulnerableTimeAfterHit = 0.1f;
        [SerializeField] private bool _isInvulnerable;

        private Health _health;
        private AudioSource _audioSource;
        private Rigidbody _rigidbody;
        private MeleeAttacker _meleeAttacker;

        [Header("Sounds")]
        [SerializeField] private AudioClip _getHitSound;

        private void Start()
        {
            _health = GetComponent<Health>();
            _audioSource = GetComponent<AudioSource>();
            _rigidbody = GetComponent<Rigidbody>();
            _meleeAttacker = GetComponent<MeleeAttacker>();

            _health.OnDie.AddListener(OnDie);
        }

        private void OnDie()
        {
            _meleeAttacker.Stop();
            Destroy(gameObject, 2f);
        }

        public void GetHit(DamageImpact impact)
        {
            if (_isInvulnerable) return;
            if (_health.IsDead) return;

            Vector3 repulsion = (transform.position - impact.Attacker.position).normalized * impact.Force;
            _rigidbody.AddForce(repulsion, ForceMode.Impulse);
            _health.DoDamage(impact);
            _meleeAttacker.OnHit();
            _audioSource.PlayOneShot(_getHitSound);
        }

        public void SetInvulnerable(bool state)
        {
            _isInvulnerable = state;
        }
    }
}
