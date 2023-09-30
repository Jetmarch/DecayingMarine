using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DecayingMarine
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private Image _healthSlider;
        [SerializeField] private Text _healthText;


        private void Start()
        {
            _playerHealth.OnHealthChanged.AddListener(OnHealthChanged);
            OnHealthChanged();
        }

        private void OnHealthChanged()
        {
            _healthText.text = _playerHealth.CurrentHealth.ToString();
            _healthSlider.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
        }
    }
}
