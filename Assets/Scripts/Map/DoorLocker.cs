using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class DoorLocker : MonoBehaviour
    {
        [SerializeField] private Door _topDoor;
        [SerializeField] private bool _isTopDoorWasClosed;
        [SerializeField] private Door _bottomDoor;
        [SerializeField] private bool _isBottomDoorWasClosed;
        [SerializeField] private Door _rightDoor;
        [SerializeField] private bool _isRightDoorWasClosed;
        [SerializeField] private Door _leftDoor;
        [SerializeField] private bool _isLeftDoorWasClosed;

        [SerializeField] private bool _isLockWasAlready;

        private void Awake()
        {
            _isLockWasAlready = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>() == null) return;
            if (_isLockWasAlready) return;
            LockDoors();
            _isLockWasAlready = true;
        }


        private void LockDoors()
        {
            _isTopDoorWasClosed = _topDoor.IsClosed;
            _isBottomDoorWasClosed = _bottomDoor.IsClosed;
            _isRightDoorWasClosed = _rightDoor.IsClosed;
            _isLeftDoorWasClosed = _leftDoor.IsClosed;

            _topDoor.Close();
            _bottomDoor.Close();
            _rightDoor.Close();
            _leftDoor.Close();
        }

        public void UnlockDoors()
        {
            if(!_isTopDoorWasClosed)
            {
                _topDoor.Open();
            }
            if(!_isBottomDoorWasClosed)
            {
                _bottomDoor.Open();
            }
            if(!_isRightDoorWasClosed)
            {
                _rightDoor.Open();
            }
            if(!_isLeftDoorWasClosed)
            {
                _leftDoor.Open();
            }
        }
    }
}
