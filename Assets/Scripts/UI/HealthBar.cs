using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DecayingMarine
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private Image _healthSlider;
        [SerializeField] private TextMeshProUGUI _healthText;


        private void Start()
        {
            _playerHealth.OnHit.AddListener(OnHealthChanged);
            _playerHealth.OnHeal.AddListener(OnHealthChanged);
            OnHealthChanged();
        }

        private void OnHealthChanged()
        {
            _healthText.text = ((int)_playerHealth.CurrentHealth).ToString();
            _healthSlider.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
        }
    }
}
