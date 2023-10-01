using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DecayingMarine
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 1f;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        public void MoveCameraToNextRoom(Vector3 newCameraPosition)
        {
            _mainCamera.transform.DOMove(newCameraPosition, _moveDuration);
        }
    }
}
