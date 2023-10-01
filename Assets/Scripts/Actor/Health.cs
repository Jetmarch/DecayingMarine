using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DecayingMarine
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private bool _isDead;

        public float CurrentHealth { get { return _currentHealth; } }
        public float MaxHealth { get { return _maxHealth; } }
        public bool IsDead { get { return _isDead; } }
        public UnityEvent OnHit;
        public UnityEvent OnHeal;
        public UnityEvent OnDie;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void DoDamage(DamageImpact impact)
        {
            if (_isDead) return;

            _currentHealth -= impact.Damage;
            _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
            OnHit?.Invoke();
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            OnDie?.Invoke();
            _isDead = true;
        }

        public void DoHeal(float healAmount)
        {
            if (_isDead) return;

            _currentHealth += healAmount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
            OnHeal?.Invoke();
        }
    }
}
