using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    [RequireComponent(typeof(Rigidbody))]
    public class ActorMove : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float _movementSpeed = 15f;
        [SerializeField] private float _maxSpeed = 50f;
        [SerializeField] private Transform _bodyForRotation;
        private Vector2 _move;

        [Header("Dashing")]
        [SerializeField] private float _dashSpeed = 20f;
        [SerializeField] private float _dashTime = 0.2f;
        [SerializeField] private bool _isDashing;
        //[SerializeField] private int _dashesAvailableCount = 1;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            UpdateMove();
        }

        private void UpdateMove()
        {
            Vector3 currentVelocity = _rb.velocity;
            Vector3 targetVelocity = new Vector3(_move.x, 0f, _move.y);
            targetVelocity = Vector3.ClampMagnitude(targetVelocity, 1f);
            if (_isDashing)
            {
                targetVelocity *= (_movementSpeed + _dashSpeed);
            }
            else
            {
                targetVelocity *= _movementSpeed;
            }

            targetVelocity = transform.TransformDirection(targetVelocity);
            Vector3 velocityChange = (targetVelocity - currentVelocity);
            velocityChange = new Vector3(velocityChange.x, 0f, velocityChange.z);
            Vector3.ClampMagnitude(velocityChange, _maxSpeed);
            _rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        private IEnumerator Dashing()
        {
            _isDashing = true;
            yield return new WaitForSeconds(_dashTime);
            _isDashing = false;
        }

        public void Move(Vector2 moveVector)
        {
            //_rb.velocity += new Vector3(moveVector.x, 0f, moveVector.y) * _movementSpeed * Time.fixedDeltaTime;
            _move = moveVector;
        }

        public void PushAway(Vector3 pushVector)
        {
            _rb.AddForce(pushVector, ForceMode.Impulse);
        }

        public void Dash()
        {
            //if(_dashesAvailableCount > 0)
            StartCoroutine(Dashing());
        }

        public void LookAt(Vector3 targetPos)
        {
            _bodyForRotation.LookAt(new Vector3(targetPos.x, 0f, targetPos.z));
        }
    }
}
