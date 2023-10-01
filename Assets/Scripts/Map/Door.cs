using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DecayingMarine
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private float _yForClosedState = -1.3f;
        [SerializeField] private float _yForOpenState = -3.4f;
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private bool _isClosed;

        private void Awake()
        {
            _isClosed = true;
        }

        public void Open()
        {
            if (!_isClosed) return;
            transform.DOMoveY(_yForOpenState, _moveDuration);
            _isClosed = false;
        }

        public void Close()
        {
            if (_isClosed) return;
            transform.DOMoveY(_yForClosedState, _moveDuration);
            _isClosed = true;
        }
    }
}
