using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DecayingMarine
{
    public class ExitRoomTrigger : MonoBehaviour
    {
       

        [SerializeField] private CameraPosition _cameraPosOnLevelPart;
        private static CameraMover _cameraMover;
       
        private void Start()
        {
            if (_cameraMover == null)
            {
                _cameraMover = Camera.main.GetComponentInChildren<CameraMover>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) return;

            _cameraMover.MoveCameraToNextRoom(_cameraPosOnLevelPart.transform.position);
        }
    }
}
