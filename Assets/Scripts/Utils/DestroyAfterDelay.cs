using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class DestroyAfterDelay : MonoBehaviour
    {
        [SerializeField] private float _delay = 2f;

        private void Start()
        {
            Destroy(gameObject, _delay);
        }
    }
}
