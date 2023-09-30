using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    [RequireComponent(typeof(AudioSource))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] protected bool _isAvailable;
        [SerializeField] protected float _weight;
        [SerializeField] protected AudioClip _pickupSound;
        public Sprite Icon { private set { _icon = value;  } get { return _icon; } }

        private void OnTriggerEnter(Collider other)
        {
            var inventory = other.GetComponent<Inventory>();
            if (inventory == null) return;

            inventory.AddItem(this);
            GetComponent<AudioSource>().PlayOneShot(_pickupSound);
        }

        public virtual void Use()
        {

        }
    }
}
