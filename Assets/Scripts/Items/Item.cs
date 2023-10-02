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
        [SerializeField] protected AudioClip _failedPickupSound;
        [SerializeField] protected AudioClip _useSound;
        [SerializeField] protected bool _isDisposable;
        [SerializeField] private GameObject _model;
        public Sprite Icon { private set { _icon = value;  } get { return _icon; } }
        public float Weight { get { return _weight; } }
        public bool IsDisposable { get { return _isDisposable; } }

        private void OnTriggerEnter(Collider other)
        {
            var inventory = other.GetComponent<Inventory>();
            if (inventory == null) return;


            bool addItemResult = inventory.AddItem(this);
            if (addItemResult)
            {
                GetComponent<AudioSource>().PlayOneShot(_pickupSound);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(_failedPickupSound);
            }
        }

        public virtual void Use()
        {

        }

        public void SetActiveAndVisible(bool state)
        {
            //_isAvailable = state;
            _model.SetActive(state);
        }
    }
}
