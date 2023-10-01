using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class FirstAidKit : Item
    {
        [SerializeField] private float _healAmount;

        private AudioSource _audio;
        private static Actor _player;
        
        private void Start()
        {
            _player = FindObjectOfType<Player>().GetComponent<Actor>();
            _audio = GetComponent<AudioSource>();
            _isAvailable = true;
            _isDisposable = true;
        }

        public override void Use()
        {
            if (!_isAvailable) return; 

            _player.GetHeal(_healAmount);
            _audio.PlayOneShot(_useSound);

            _isAvailable = false;

            Destroy(gameObject, 0.7f);
        }
    }
}
