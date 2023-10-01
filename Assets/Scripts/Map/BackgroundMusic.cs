using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
