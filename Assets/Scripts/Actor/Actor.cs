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

        private AudioSource _audioSource;

        [Header("Sounds")]
        [SerializeField] private AudioClip _moveSound;
        [SerializeField] private AudioClip _dashSound;
        [SerializeField] private AudioClip _changeCurrentItemSound;
        [SerializeField] private AudioClip _getHitSound;

        private void Start()
        {
            _actorMove = GetComponent<ActorMove>();
            _itemUser = GetComponent<ItemUser>();
            _health = GetComponent<Health>();
            _audioSource = GetComponent<AudioSource>();
        }

        public void Move(Vector2 moveVector)
        {
            _actorMove.Move(moveVector);

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

        public void GetHit(DamageImpact impact)
        {
            Vector3 repulsion = (transform.position - impact.Attacker.position).normalized * impact.Force;
            _health.DoDamage(impact);
            _actorMove.PushAway(repulsion);
            _audioSource.PlayOneShot(_getHitSound);
        }
    }
}
