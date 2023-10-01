using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace DecayingMarine
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private float _shakeStrength;

        private Vector3 _previousCameraPosition;

        private void Start()
        {
            _playerHealth.OnHit.AddListener(ShakeCamera);
            _mainCamera = Camera.main;
            DOTween.Init();
        }

        private void ShakeCamera()
        {
            _mainCamera.transform.DOShakePosition(_shakeDuration, _shakeStrength);
        }
    }
}
