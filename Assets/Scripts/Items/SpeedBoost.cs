using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class SpeedBoost : Item
    {
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private float _buffTime = 20f;

        [SerializeField] private AudioClip _buffEndSound;

        private AudioSource _audio;
        private static ActorMove _playerMovement;
        private void Start()
        {
            _playerMovement = FindObjectOfType<Player>().GetComponent<ActorMove>();
            _audio = GetComponent<AudioSource>();
            _isAvailable = true;
            _isDisposable = true;
        }

        public override void Use()
        {
            if (!_isAvailable) return;

            StartCoroutine(Boost());
            _audio.PlayOneShot(_useSound);

            _isAvailable = false;
        }

        private IEnumerator Boost()
        {
            float prevPlayerSpeed = _playerMovement.MovementSpeed;
            _playerMovement.ChangeMovementSpeed(_playerMovement.MovementSpeed * _speedMultiplier);

            yield return new WaitForSeconds(_buffTime);

            _playerMovement.ChangeMovementSpeed(prevPlayerSpeed);
            _audio.PlayOneShot(_buffEndSound);
        }
    }
}
