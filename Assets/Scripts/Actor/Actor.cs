using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    [RequireComponent(typeof(ActorMove))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(AudioSource))]
    public class Actor : MonoBehaviour
    {
        private ActorMove _actorMove;
        private ItemUser _itemUser;
        private Health _health;
        private Inventory _inventory;

        private AudioSource _audioSource;

        [SerializeField] private float _tempInvulnerability = 0.2f;
        [SerializeField] private bool _isInvulnerable;
        [SerializeField] private bool _isTooHeavyWeight;

        [SerializeField] private GameObject _punchDamageZone;
        [SerializeField] private float _punchDamageZoneTime = 0.2f;

        [Header("Sounds")]
        [SerializeField] private AudioClip _moveSound;
        [SerializeField] private AudioClip _dashSound;
        [SerializeField] private AudioClip _changeCurrentItemSound;
        [SerializeField] private AudioClip _getHitSound;
        [SerializeField] private AudioClip _punchSound;
        [SerializeField] private AudioClip _tooHeavyInventorySound;
        [SerializeField] private AudioClip _okItemsWeightSound;

        private void Start()
        {
            _actorMove = GetComponent<ActorMove>();
            _itemUser = GetComponent<ItemUser>();
            _health = GetComponent<Health>();
            _audioSource = GetComponent<AudioSource>();
            _inventory = GetComponent<Inventory>();
            _inventory.OnTooHeavyInventory.AddListener(OnTooHeavyInventory);
            _inventory.OnOkInventoryWeight.AddListener(OnOkInventoryWeight);
        }

        private IEnumerator TempInvulnerability()
        {
            _isInvulnerable = true;
            yield return new WaitForSeconds(_tempInvulnerability);
            _isInvulnerable = false;
        }

        private void OnTooHeavyInventory()
        {
            _isTooHeavyWeight = true;
            _audioSource.PlayOneShot(_tooHeavyInventorySound);
        }

        private void OnOkInventoryWeight()
        {
            _isTooHeavyWeight = false;
            _audioSource.PlayOneShot(_okItemsWeightSound);
        }

        private IEnumerator RemovePunchDamageZone()
        {
            yield return new WaitForSeconds(_punchDamageZoneTime);
            _punchDamageZone.SetActive(false);
        }

        public void Move(Vector2 moveVector)
        {
            if (_isTooHeavyWeight)
            {
                _actorMove.Move(moveVector * 0f);
            }
            else
            {
                _actorMove.Move(moveVector);
            }

            if(moveVector.magnitude > 0.1f)
            {
                if (_audioSource.isPlaying) return;
                _audioSource.PlayOneShot(_moveSound);
            }
        }

        public void Dash()
        {
            _actorMove.Dash();
            _audioSource.PlayOneShot(_dashSound);
        }

        public void Punch()
        {
            if (_punchDamageZone.activeSelf == true) return;

            _punchDamageZone.SetActive(true);
            StartCoroutine(RemovePunchDamageZone());
        }

        public void LookAt(Vector3 target)
        {
            _actorMove.LookAt(target);
        }

        public void ChangeCurrentItem(int index)
        {
            _itemUser.ChangeCurrentItem(index);
            _audioSource.PlayOneShot(_changeCurrentItemSound);
        }

        public void UseCurrentItem()
        {
            _itemUser.UseCurrentItem();
        }

        public void DropCurrentItem()
        {
            _itemUser.DropCurrentItem();
        }

        public void GetHit(DamageImpact impact)
        {
            if (_isInvulnerable) return;
            StartCoroutine(TempInvulnerability());
            Vector3 repulsion = (transform.position - impact.Attacker.position).normalized * impact.Force;
            _health.DoDamage(impact);
            _actorMove.PushAway(repulsion);
            _audioSource.PlayOneShot(_getHitSound);
        }

        public void GetHeal(float healAmount)
        {
            _health.DoHeal(healAmount);
        }
    }
}
